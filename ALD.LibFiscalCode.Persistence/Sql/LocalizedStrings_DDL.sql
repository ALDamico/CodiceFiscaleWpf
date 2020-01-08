CREATE TABLE LocalizedStrings
(
    id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    name TEXT,
    value TEXT,
    language_id INTEGER,
    FOREIGN KEY (language_id) REFERENCES Languages(id)
);

CREATE INDEX idx_fk_loc_strings ON LocalizedStrings(language_id);