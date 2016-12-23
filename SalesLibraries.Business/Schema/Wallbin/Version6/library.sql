CREATE TABLE [Library1] (
[Id] INTEGER  NOT NULL PRIMARY KEY,
[ExtId] BLOB  NOT NULL,
[SyncDate] DATETIME,
[LastModified] DATETIME,
[SettingsEncoded] TEXT,
[SyncSettingsEncoded] TEXT,
[ProgramDataEncoded] TEXT,
[CalendarEncoded] TEXT
);
INSERT INTO [Library1] 
SELECT 
	[Id],
	[ExtId],
	[SyncDate],
	[LastModified],
	[SettingsEncoded],
	[SyncSettingsEncoded],
	[ProgramDataEncoded],
	[CalendarEncoded]
FROM [Library];
DROP TABLE [Library];
CREATE TABLE [Library] (
[Id] INTEGER  NOT NULL PRIMARY KEY,
[ExtId] BLOB  NOT NULL,
[SyncDate] DATETIME,
[LastModified] DATETIME,
[SettingsEncoded] TEXT,
[SyncSettingsEncoded] TEXT,
[ProgramDataEncoded] TEXT,
[CalendarEncoded] TEXT
);
INSERT INTO [Library] 
SELECT 
	[Id],
	[ExtId],
	[SyncDate],
	[LastModified],
	[SettingsEncoded],
	[SyncSettingsEncoded],
	[ProgramDataEncoded],
	[CalendarEncoded]
FROM [Library1];
DROP TABLE [Library1];