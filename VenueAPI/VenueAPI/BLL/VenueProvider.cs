using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using VenueAPI.DAL;

namespace VenueAPI.BLL
{
    class VenueProvider : IVenueProvider
    {
        private readonly IVenueRepository _venueRepo;
        public VenueProvider(IVenueRepository venueRepo)
        {
            _venueRepo = venueRepo;
        }

        public async Task<bool> AddVenueAsync()
        {
            return await _venueRepo.AddVenueAsync();
        }
    }
}
