using GoMontag_WebApp.Client.ClientComponentServices.Interface;

namespace GoMontag_WebApp.Client.ClientComponentServices
{
    public class UserTimeZoneClientService : IUserTimeZoneClientService
    {
        public string TimeZoneId { get; set; } = "UTC";

        public DateTimeOffset ConvertToUserTime(DateTime utcDateTime)
        {
            if (TimeZoneId == "UTC")
                return new DateTimeOffset(utcDateTime, TimeSpan.Zero);

            var tz = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneId);
            var local = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, tz);
            var offset = tz.GetUtcOffset(local);
            return new DateTimeOffset(local, offset);
        }
    }
}
