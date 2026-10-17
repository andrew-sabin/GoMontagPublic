namespace GoMontag_WebApp.Client.ClientComponentServices.Interface
{
    public interface IUserTimeZoneClientService
    {
        string TimeZoneId { get; set; }
        DateTimeOffset ConvertToUserTime(DateTime utcDateTime);
    }
}
