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

        public async Task<VenueDto> GetVenueAsync(Guid venueId)
        {
            return await _venueRepo.GetVenueAsync(venueId);
        }

        public async Task<List<VenueDto>> GetVenuesAsync()
        {
            return await _venueRepo.GetVenuesAsync();
        }

        public async Task<VenueDto> EditVenueAsync(Venue venue, Guid venueId)
        {
            return await _venueRepo.EditVenueAsync(venue, venueId);
        }

        public async Task<bool> DeleteVenueAsync(Guid venueId)
        {
            return await _venueRepo.DeleteVenueAsync(venueId);
        }


        public async Task<SpaceDto> AddSpaceAsync(Space space, Guid venueId)
        {
            return await _venueRepo.AddSpaceAsync(space, venueId);
        }

        public async Task<SpaceDto> GetSpaceAsync(Guid spaceId)
        {
            return await _venueRepo.GetSpaceAsync(spaceId);
        }
        public async Task<List<SpaceDto>> GetSpacesAsync(Guid venueId)
        {
            return await _venueRepo.GetSpacesAsync(venueId);
        }

        public async Task<SpaceDto> EditSpaceAsync(Space space, Guid spaceId, Guid venueId)
        {
            return await _venueRepo.EditSpaceAsync(space, spaceId, venueId);
        }

        public async Task<bool> DeleteSpaceAsync(Guid spaceId, Guid venueId)
        {
            return await _venueRepo.DeleteSpaceAsync(spaceId, venueId);
        }

        public async Task<List<SpaceImageDto>> AddSpaceImagesAsync(List<SpaceImage> spaceImages, Guid spaceId)
        {
            return await _venueRepo.AddSpaceImagesAsync(spaceImages, spaceId);
        }
        public async Task<List<SpaceImageDto>> GetSpaceImagesAsync(Guid spaceId)
        {
            return await _venueRepo.GetSpaceImagesAsync(spaceId);
        }
        public async Task<bool> DeleteSpaceImagesAsync(List<Guid> spaceImageIds, Guid spaceId)
        {
            return await _venueRepo.DeleteSpaceImagesAsync(spaceImageIds, spaceId);
        }
    }
}
