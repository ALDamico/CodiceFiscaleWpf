CREATE TABLE Settings
(
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT,
    int_value INTEGER,
    string_value TEXT
);

CREATE INDEX idx_settings_int ON Settings(int_value);
CREATE INDEX idx_settings_string ON Settings(string_value);

INSERT INTO Settings(name, int_value, string_value)
VALUES ('AppLanguage', 1, NULL),
('DataSourceLocation', NULL, '.'),
('MaxHistorySize', 0, NULL),
('DefaultDate', NULL, NULL);