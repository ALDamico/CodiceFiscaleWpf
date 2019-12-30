CREATE TABLE Places(
        id INTEGER PRIMARY KEY AUTOINCREMENT,
        name TEXT,
        province_name TEXT,
        province_abbreviation TEXT,
        region_name TEXT,
        code TEXT
    );

CREATE INDEX idx_prov_abbreviation ON Places(province_abbreviation);
CREATE INDEX idx_region ON Places(region_name);
