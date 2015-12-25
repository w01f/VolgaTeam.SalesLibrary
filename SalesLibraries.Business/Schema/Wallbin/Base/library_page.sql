CREATE TABLE [LibraryPage] (
[Id] INTEGER  NOT NULL PRIMARY KEY,
[ExtId] BLOB  NOT NULL,
[Library_Id] INTEGER  NOT NULL REFERENCES [Library]([Id]),
[Name] VARCHAR(128) NOT NULL,
[Order] INTEGER NOT NULL,
[LastModified] DATETIME,
[SettingsEncoded] TEXT
)