using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VenueAPI.Extensions;
using VLibraries.APIAbstractions;
using VLibraries.APIModels;
using VLibraries.APIModels.GeoJson;

namespace VenueAPI.Services.LocationIq
{
    public class LocationIqProvider : ILocationIqProvider
    {
        private readonly ILocationIqProxy _locationIqProxy;
        private readonly ILocationIqRepository _locationRepo;

        public LocationIqProvider(ILocationIqProxy locationIqProxy, ILocationIqRepository locationRepo)
        {
            _locationIqProxy = locationIqProxy;
            _locationRepo = locationRepo;
        }

        public async Task<VenueLocation> AddGeoLocation(VenueLocation venueLocation)
        {
            VenueLocation location = await _locationRepo.AddGeoLocation(venueLocation);

            return location;
        }

        public async Task<LocationIqReverseResponse> GetLocationDetailsAsync(string postcode)
        {
            LocationIqReverseResponse locationIqResponse = await _locationIqProxy.GetLocationDetailsAsync(postcode);

            return locationIqResponse;
        }
    }   
}
