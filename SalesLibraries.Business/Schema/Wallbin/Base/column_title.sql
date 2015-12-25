CREATE TABLE [ColumnTitle] (
[Id] INTEGER  NOT NULL PRIMARY KEY,
[ExtId] BLOB  NOT NULL,
[Page_Id] INTEGER NOT NULL REFERENCES [LibraryPage]([Id]),
[ColumnOrder] INTEGER NOT NULL,
[LastModified] DATETIME,
[SettingsEncoded] TEXT,
[WidgetEncoded] TEXT,
[BannerEncoded] TEXT
)