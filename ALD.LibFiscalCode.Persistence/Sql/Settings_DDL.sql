CREATE TABLE Settings
(
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    int_value INTEGER,
    string_value TEXT
);

CREATE INDEX idx_settings_int ON Settings(int_value);
CREATE INDEX idx_settings_string ON Settings(string_value);