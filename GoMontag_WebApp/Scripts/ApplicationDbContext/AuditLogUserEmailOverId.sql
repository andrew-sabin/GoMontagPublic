BEGIN TRANSACTION;
EXEC sp_rename N'[AuditLogs].[UserId]', N'UserEmail', 'COLUMN';

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20261008195121_ChangeUserIdToUserEmailForAuditLog', N'10.0.10');

COMMIT;
GO

