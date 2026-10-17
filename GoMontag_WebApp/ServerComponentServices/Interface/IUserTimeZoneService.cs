namespace GoMontag_WebApp.ServerComponentServices.Interface
{
    public interface IUserTimeZoneService
    {
        string TimeZoneId { get; set; }
        DateTimeOffset ConvertToUserTime(DateTime utcDateTime);
    }
}
