BEGIN TRANSACTION;
ALTER TABLE [FormatImages] ADD [LastUpdatedDate] datetime2 NULL;

ALTER TABLE [FormatImages] ADD [LastUserUpdate] nvarchar(max) NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260927031000_FormatImageUpdateInfo', N'10.0.10');

COMMIT;
GO

BEGIN TRANSACTION;
ALTER TABLE [FormatImages] ADD [UpdatedAltText] nvarchar(max) NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260927031819_FormatImageUpdateNewAltText', N'10.0.10');

COMMIT;
GO

