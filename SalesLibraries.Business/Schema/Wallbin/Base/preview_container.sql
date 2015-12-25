CREATE TABLE [PreviewContainer] (
[Id] INTEGER  NOT NULL PRIMARY KEY,
[ExtId] BLOB  NOT NULL,
[Library_Id] INTEGER  NOT NULL REFERENCES [Library]([Id]),
[RelativePath] VARCHAR(256) NOT NULL,
[LastModified] DATETIME,
[Discriminator] VARCHAR(128) NOT NULL
)