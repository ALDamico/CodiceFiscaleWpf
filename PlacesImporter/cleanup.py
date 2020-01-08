#!/usr/bin/env python3
# This file is used for cleaning up dirty data from the application's database

import sqlite3
import csv
import logging
from datetime import datetime
from shutil import copy


def execute_query(conn, query):
    logging.info("Executing query")
    logging.info(query)
    conn.execute(query)


class Place(object):
    name = ""
    province = ""
    province_abbreviation = ""
    region = ""
    code = ""

    def __init__(self, name, province, province_abbreviation, region, code):
        self.name = name
        self.province = province
        self.province_abbreviation = province_abbreviation
        self.region = region
        self.code = code

    def get_as_tuple(self):
        return (self.name, self.province, self.province_abbreviation, self.region, self.code)


start_time = datetime.now()

log_file = "cleanup.log"
csv_file_name = "places.csv"
logging.basicConfig(filename=log_file, level=logging.INFO, filemode='w')
logging.info("Started cleanup at {}".format(start_time))

database_file = "./app.db"
logging.info(
    "Attempting to connect to database '{}'".format(database_file))
conn = sqlite3.connect(database_file)

drop_queries = (
    "DROP TABLE FiscalCodes",
    "DROP TABLE People",
    "DROP TABLE Places",
    "DROP TABLE Languages",
    "DROP TABLE LocalizedStrings",
    "DROP TABLE Settings"
)


for query in drop_queries:
    try:
        conn.execute(query)
    except sqlite3.OperationalError as e:
        logging.info("{}. Skipping.".format(e))


logging.info("Tables dropped. Recreating table structures")

ddl_queries = (
    """
        CREATE TABLE FiscalCodes
        (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            fiscal_code TEXT,
            person_id INTEGER,
            FOREIGN KEY (person_id) REFERENCES People(id)
        )
    """,
    """
        CREATE TABLE People 
        (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            name TEXT,
            surname TEXT,
            date_of_birth TEXT,
            place_of_birth_id INTEGER,
            gender TEXT,
            FOREIGN KEY(place_of_birth_id) REFERENCES Places(id)
        )
    """,
    """
        CREATE TABLE Places
        (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            name TEXT,
            province_name TEXT,
            province_abbreviation TEXT,
            region_name TEXT,
            code TEXT
        )
    """,
    """
        CREATE TABLE Languages
        (
            id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
            name TEXT,
            iso_2_code TEXT CHECK(LENGTH(iso_2_code) == 2),
            iso_3_code TEXT CHECK(LENGTH(iso_3_code) == 3)
        )
    """,
    """
        CREATE TABLE Windows
        (
            id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
            window_name TEXT
        )
    """,
    """
        CREATE TABLE LocalizedStrings
        (
            id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
            name TEXT,
            value TEXT,
            language_id INTEGER,
            window_id INTEGER,
            FOREIGN KEY (language_id) REFERENCES Languages(id) ON DELETE SET NULL,
            FOREIGN KEY (window_id) REFERENCES Windows(id) ON DELETE SET NULL
        )
    """,
    """
        CREATE TABLE Settings
        (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            int_value INTEGER,
            string_value TEXT
        )
    """
)

for query in ddl_queries:
    execute_query(conn, query)

languages_default_values = (
    {
        "name": "Italiano",
        "iso_2_code": "it",
        "iso_3_code": "ita"
    },
    {
        "name": "English",
        "iso_2_code": "en",
        "iso_3_code": "eng"
    }
)

for el in languages_default_values:
    logging.info("Executing query")
    query = """
        INSERT INTO Languages(name, iso_2_code, iso_3_code)
        VALUES (?, ?, ?)
    """
    logging.info(query)
    conn.execute(query, (el["name"], el["iso_2_code"], el["iso_3_code"]))
    
conn.commit()

with open(csv_file_name, encoding='utf-8') as csv_file:
    dialect = csv.Sniffer().sniff(csv_file.read(1024))
    place_reader = csv.reader(csv_file, dialect)

    csv_file.seek(0)
    next(place_reader, None)
    for row in place_reader:
        logging.info("Inserting place {}".format(row[0]))
        name = row[0]
        province = row[1]
        province_abbreviation = row[2]
        region_name = row[3]
        code = row[4]
        place = Place(name, province, province_abbreviation, region_name, code)
        query = """
            INSERT INTO Places(name, province_name, province_abbreviation, region_name, code)
            VALUES (?, ?, ?, ?, ?)
        """
        conn.execute(query, place.get_as_tuple())
    conn.commit()

# Indices
index_queries = (
    "CREATE INDEX idx_fk_loc_strings ON LocalizedStrings(language_id)",
    "CREATE INDEX idx_place_id ON People(place_of_birth_id)",
    "CREATE INDEX idx_prov_abbreviation ON Places(province_abbreviation)",
    "CREATE INDEX idx_region ON Places(region_name)",
    "CREATE INDEX idx_win_id ON LocalizedStrings(window_id)",
    "CREATE INDEX idx_settings_int ON Settings(int_value)",
    "CREATE INDEX idx_settings_string ON Settings(string_value)"
)
for query in index_queries:
    execute_query(conn, query)

conn.commit()
conn.close()
logging.debug("Connection to {} closed".format(database_file))

destination = "../ALD.LibFiscalCode.Persistence/DataSource"
logging.debug("Copying {} to {}".format(database_file, destination))
try:
    copy(database_file, destination)
    logging.debug("Done.")
except Exception as e:
    logging.exception(e)

logging.info("Completed at {}".format(datetime.now()))
logging.shutdown()
exit(0)
