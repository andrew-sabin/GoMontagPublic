BEGIN TRANSACTION;
ALTER TABLE [FormatImages] ADD [CreatedDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';

ALTER TABLE [FormatImages] ADD [ImageName] nvarchar(max) NOT NULL DEFAULT N'';

ALTER TABLE [FormatImages] ADD [ModifiedDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';

ALTER TABLE [FormatImages] ADD [PreviousTimes] nvarchar(max) NOT NULL DEFAULT N'[]';

ALTER TABLE [FormatImages] ADD [PreviousUrls] nvarchar(max) NOT NULL DEFAULT N'[]';

ALTER TABLE [FormatImages] ADD [PreviousUsers] nvarchar(max) NOT NULL DEFAULT N'[]';

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260922212139_UpdateFormatImageWithImageNameAndPreviousImageUrls', N'10.0.10');

COMMIT;
GO

BEGIN TRANSACTION;
DECLARE @var nvarchar(max);
SELECT @var = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[FormatImages]') AND [c].[name] = N'PreviousTimes');
IF @var IS NOT NULL EXEC(N'ALTER TABLE [FormatImages] DROP CONSTRAINT ' + @var + ';');
ALTER TABLE [FormatImages] DROP COLUMN [PreviousTimes];

DECLARE @var1 nvarchar(max);
SELECT @var1 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[FormatImages]') AND [c].[name] = N'PreviousUrls');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [FormatImages] DROP CONSTRAINT ' + @var1 + ';');
ALTER TABLE [FormatImages] DROP COLUMN [PreviousUrls];

DECLARE @var2 nvarchar(max);
SELECT @var2 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[FormatImages]') AND [c].[name] = N'PreviousUsers');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [FormatImages] DROP CONSTRAINT ' + @var2 + ';');
ALTER TABLE [FormatImages] DROP COLUMN [PreviousUsers];

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260922213449_UpdateFormatImageRemovePreviousUrlsTimesAndUsers', N'10.0.10');

COMMIT;
GO

BEGIN TRANSACTION;
ALTER TABLE [FormatImages] ADD [Width] int NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260923224440_AddFormatImageWidth', N'10.0.10');

COMMIT;
GO

BEGIN TRANSACTION;
DECLARE @var3 nvarchar(max);
SELECT @var3 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[FormatImages]') AND [c].[name] = N'ImageUrl');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [FormatImages] DROP CONSTRAINT ' + @var3 + ';');
ALTER TABLE [FormatImages] ALTER COLUMN [ImageUrl] nvarchar(450) NOT NULL;

ALTER TABLE [FormatImages] ADD [DeletedDate] datetime2 NULL;

ALTER TABLE [FormatImages] ADD [isDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);

CREATE UNIQUE INDEX [IX_FormatImages_ImageUrl] ON [FormatImages] ([ImageUrl]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260925192711_MakeFormatImagesSoftDeleteAble', N'10.0.10');

COMMIT;
GO

