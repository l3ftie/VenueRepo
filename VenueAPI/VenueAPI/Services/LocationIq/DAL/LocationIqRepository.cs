using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace VenueAPI.Services.LocationIq
{
    public class LocationIqRepository : ILocationIqRepository
    {
        private readonly IMongoCollection<VenueLocation> _venueLocationCollection;

        public LocationIqRepository(IConfiguration config)
        {
            _venueLocationCollection = new MongoClient(config.GetConnectionString("LocalMongoServer"))
                .GetDatabase("VenueLocation")
                .GetCollection<VenueLocation>("VenueLocation");
        }

        public async Task<VenueLocation> AddGeoLocation(VenueLocation venueLocation)
        {
            await _venueLocationCollection.InsertOneAsync(venueLocation);

            return venueLocation;
        }
    }
}
