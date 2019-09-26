using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using VenueAPI.DAL;
using VLibraries.APIModels;

namespace VenueAPI.BLL
{
    class VenueProvider : IVenueProvider
    {
        private readonly IVenueRepository _venueRepo;
        public VenueProvider(IVenueRepository venueRepo)
        {
            _venueRepo = venueRepo;
        }

        public async Task<Venue> AddVenueAsync(Venue venue)
        {
            return await _venueRepo.AddVenueAsync(venue);
        }
    }
}
