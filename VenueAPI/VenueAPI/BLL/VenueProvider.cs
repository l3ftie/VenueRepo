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

        public async Task<VenueResponse> AddVenueAsync(VenueRequest venue)
        {
            return await _venueRepo.AddVenueAsync(venue);
        }

        public async Task<VenueResponse> GetVenueAsync(Guid venueId)
        {
            return await _venueRepo.GetVenueAsync(venueId);
        }

        public async Task<List<VenueDto>> GetVenuesAsync()
        {
            return await _venueRepo.GetVenuesAsync();
        }

        public async Task<VenueResponse> EditVenueAsync(VenueRequest venue, Guid venueId)
        {
            return await _venueRepo.EditVenueAsync(venue, venueId);
        }

        public async Task<bool> DeleteVenueAsync(Guid venueId)
        {
            return await _venueRepo.DeleteVenueAsync(venueId);
        }        
    }
}
