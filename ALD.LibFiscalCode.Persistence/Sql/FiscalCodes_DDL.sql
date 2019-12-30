CREATE TABLE FiscalCodes
(
	id INTEGER PRIMARY KEY AUTOINCREMENT,
	fiscal_code TEXT,
	person_id INTEGER,
	FOREIGN KEY (person_id) REFERENCES People(id)
);

CREATE INDEX idx_people_fiscal_codes ON FiscalCodes(person_id);