CREATE TABLE [Library] (
[Id] INTEGER  NOT NULL PRIMARY KEY,
[ExtId] BLOB  NOT NULL,
[BrandingText] VARCHAR(128),
[SyncDate] DATETIME,
[LastModified] DATETIME,
[SettingsEncoded] TEXT,
[SyncSettingsEncoded] TEXT,
[InactiveLinksEncoded] TEXT,
[ProgramDataEncoded] TEXT,
[CalendarEncoded] TEXT
)