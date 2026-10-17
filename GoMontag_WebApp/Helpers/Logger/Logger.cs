namespace GoMontag_WebApp.Helpers.Logger
{
    public static partial class Logger
    {
        /* Products */
        [LoggerMessage(15, LogLevel.Information, "Product {productName} created by {userName} at {ipAddress} on {date}. ")]
        public static partial void CreateProduct(this ILogger logger, DateTime date, string productName, string ipAddress, string userName);
        [LoggerMessage(20, LogLevel.Information, "Product {productName} edited by {userName} at {ipAddress} on {date}. ")]
        public static partial void EditProduct(this ILogger logger, DateTime date, string productName, string ipAddress, string userName);
        [LoggerMessage(10, LogLevel.Warning, "Product {id} deleted by {userName} at {ipAddress} on {date}. ")]
        public static partial void DeleteProduct(this ILogger logger, DateTime date, int id, string ipAddress, string userName);
        /* Formats */
        [LoggerMessage(15, LogLevel.Information, "Format {formatType} - {formatEdition} for {productId} was created by {userName} at {ipAddress} on {date}.")]
        public static partial void CreateFormat(this ILogger logger, DateTime date, int productId, string formatType, string formatEdition, string ipAddress, string userName);
        [LoggerMessage(20, LogLevel.Information, "Format {formatType} - {formatEdition} for {productId} was edited by {userName} at {ipAddress} on {date}.")]
        public static partial void EditFormat(this ILogger logger, DateTime date, int productId, string formatType, string formatEdition, string ipAddress, string userName);
        [LoggerMessage(10, LogLevel.Information, "Format {id} was deleted by {userName} at {ipAddress} on {date}.")]
        public static partial void DeleteFormat(this ILogger logger, DateTime date, int id, string ipAddress, string userName);
        /* Log-in and Register */
        [LoggerMessage(20, LogLevel.Information, "User {userName} has logged in from {ipAddress} at {date}.")]
        public static partial void LogInLogger(this ILogger logger, DateTime date, string userName, string ipAddress);
        [LoggerMessage(20, LogLevel.Information, "User {userName} has registered their email {email} from {ipAddress} at {date}.")]
        public static partial void Register(this ILogger logger, DateTime date, string userName, string email, string ipAddress);
        [LoggerMessage(15, LogLevel.Warning, "User {userName} has reset their Password from {ipAddress} at {date}.")]
        public static partial void ResetPassword(this ILogger logger, DateTime date, string userName, string ipAddress);
        /* User Account Changes */
        [LoggerMessage(15, LogLevel.Warning, "User {userName} has changed their email to {Email} and DisplayName to {DisplayName} from {ipAddress} on {date}")]
        public static partial void EditAccountInfo(this ILogger logger, DateTime date, string userName, string DisplayName, string Email, string ipAddress);
        /* File Upload */
        [LoggerMessage(20, LogLevel.Information, "User {userName} has uploaded {imageName} to {blobPath} from {ipAddress} on {date}. ")]
        public static partial void UploadImageInfo(this ILogger logger, DateTime date, string userName, string ipAddress, string imageName, string blobPath);
        [LoggerMessage(20, LogLevel.Information, "User {userName} has uploaded {fileName} to {blobPath} from {ipAddress} on {date}. ")]
        public static partial void UploadFileInfo(this ILogger logger, DateTime date, string userName, string ipAddress, string fileName, string blobPath);
        [LoggerMessage(15, LogLevel.Information, "User {userName} has created {imageUrl} from {ipAddress} on {date}. ")]
        public static partial void CreateFormatImage(this ILogger logger, DateTime date, string userName, string ipAddress, string imageUrl);
        [LoggerMessage(15, LogLevel.Warning, "User {username} has deleted {imageId} from {ipAddress} on {date}. ")]
        public static partial void DeleteFormatImage(this ILogger logger, DateTime date, string userName, string ipAddress, int imageId);
    }
}
