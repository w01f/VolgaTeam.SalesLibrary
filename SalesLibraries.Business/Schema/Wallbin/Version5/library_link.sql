UPDATE [LibraryLink] SET [SettingsEncoded] = NULL WHERE [Discriminator] = 'InternalLink';
UPDATE [LibraryLink] SET [Discriminator] = 'InternalWallbinLink' WHERE [Discriminator] = 'InternalLink'