using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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

        public async Task<VenueDto> AddVenueAsync(Venue venue)
        {
            return await _venueRepo.AddVenueAsync(venue);
        }

        public async Task<VenueDto> GetVenueAsync(Guid id)
        {
            return await _venueRepo.GetVenueAsync(id);
        }

        public async Task<List<VenueDto>> GetVenuesAsync()
        {
            return await _venueRepo.GetVenuesAsync();
        }

        public async Task<VenueDto> EditVenueAsync(Venue venue, Guid id)
        {
            return await _venueRepo.EditVenueAsync(venue, id);
        }

        public async Task<bool> DeleteVenueAsync(Guid id)
        {
            return await _venueRepo.DeleteVenueAsync(id);
        }


        public async Task<VenueDto> AddSpaceAsync(Space space, Guid venueId)
        {
            return await _venueRepo.AddSpaceAsync(space, venueId);
        }
    }
}
