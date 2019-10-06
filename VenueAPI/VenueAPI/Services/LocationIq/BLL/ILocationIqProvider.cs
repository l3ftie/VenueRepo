using System;
using System.Threading.Tasks;
using VLibraries.APIModels;
using VLibraries.APIModels.GeoJson;

namespace VenueAPI.Services.LocationIq
{
    public interface ILocationIqProvider
    {
        Task<VenueLocation> AddGeoLocation(VenueLocation venueLocation);
        Task<LocationIqReverseResponse> GetLocationDetailsAsync(string postcode);
    }
}
