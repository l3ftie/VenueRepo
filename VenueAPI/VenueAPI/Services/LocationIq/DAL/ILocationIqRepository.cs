using System.Threading.Tasks;
using VLibraries.APIModels.GeoJson;

namespace VenueAPI.Services.LocationIq
{
    public interface ILocationIqRepository
    {
        Task<VenueLocation> AddGeoLocation(VenueLocation venueLocation);
    }
}
