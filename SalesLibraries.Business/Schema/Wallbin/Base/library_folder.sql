CREATE TABLE [LibraryFolder] (
[Id] INTEGER  NOT NULL PRIMARY KEY,
[ExtId] BLOB  NOT NULL,
[Page_Id] INTEGER  NOT NULL REFERENCES [LibraryPage]([Id]),
[Name] VARCHAR(128) NOT NULL,
[RowOrder] INTEGER NOT NULL,
[ColumnOrder] INTEGER NOT NULL,
[AddDate] DATETIME,
[LastModified] DATETIME,
[SettingsEncoded] TEXT,
[WidgetEncoded] TEXT,
[BannerEncoded] TEXT
)