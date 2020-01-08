CREATE TABLE Languages
(
	id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	name TEXT,
	iso_2_code TEXT CHECK(LENGTH(iso_2_code) == 2),
	iso_3_code TEXT CHECK(LENGTH(iso_3_code) == 3)
);

INSERT INTO Languages(name, iso_2_code, iso_3_code)
VALUES ('Italiano', 'it', 'ita'),
('English', 'en', 'eng');