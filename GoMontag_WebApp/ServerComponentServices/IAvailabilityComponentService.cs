using GoMontag.Shared.Models.Availability;
using GoMontag.Shared.Models.Format;
namespace GoMontag_WebApp.ServerComponentServices
{
    public interface IAvailabilityComponentService
    {
        event Action? OnChange;
        List<AvailabilityResponse> Availabilities { get; set; }
        Task GetAvailabilitiesByFormatId(int productId);
        Task<AvailabilityResponse?> GetAvailabilityByID(int availId);
        Task CreateAvailability(AvailabilityRequest availability);
        Task UpdateAvailability(int availabilityID, AvailabilityRequest availability);
        Task DeleteAvailability(int availabilityID);
    }
}
