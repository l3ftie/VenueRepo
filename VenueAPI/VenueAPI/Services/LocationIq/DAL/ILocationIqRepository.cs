using System.Threading.Tasks;

namespace VenueAPI.Services.LocationIq
{
    public interface ILocationIqRepository
    {
        Task<VenueLocation> AddGeoLocation(VenueLocation venueLocation);
    }
}
