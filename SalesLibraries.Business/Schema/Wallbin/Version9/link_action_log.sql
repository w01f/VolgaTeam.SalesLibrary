CREATE TABLE [LinkActionLog] (
[Id] INTEGER  NOT NULL PRIMARY KEY,
[Library_Id] INTEGER  NOT NULL REFERENCES [Library]([Id]),
[ActionType] INTEGER NOT NULL,
[Date] DATETIME,
[Order] INTEGER NOT NULL,
[SettingsEncoded] TEXT
)