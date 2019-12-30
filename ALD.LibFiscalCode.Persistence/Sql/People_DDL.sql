CREATE TABLE People (
	id INTEGER PRIMARY KEY AUTOINCREMENT,
	name TEXT,
	surname TEXT,
	date_of_birth TEXT,
	"PlaceOfBirthId" INTEGER,
	gender TEXT,
	FOREIGN KEY("PlaceOfBirthId") REFERENCES Places(id)
);

CREATE INDEX idx_place_id ON People("PlaceOfBirthId");
