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

        public async Task<List<VenueImageDto>> AddVenueImagesAsync(List<string> base64EncodedVenueImages, Guid venueId)
        {
            return await _venueRepo.AddVenueImagesAsync(base64EncodedVenueImages, venueId);
        }

        public async Task<List<VenueImageDto>> GetVenueImagesAsync(Guid venueId)
        {
            return await _venueRepo.GetVenueImagesAsync(venueId);
        }

        public async Task<bool> DeleteVenueImagesAsync(List<Guid> venueImageIds, Guid venueId)
        {
            return await _venueRepo.DeleteVenueImagesAsync(venueImageIds, venueId);
        }

        public async Task<SpaceDto> AddSpaceAsync(Space space, Guid venueId)
        {
            return await _venueRepo.AddSpaceAsync(space, venueId);
        }

        public async Task<SpaceDto> GetSpaceAsync(Guid venueId, Guid spaceId)
        {
            return await _venueRepo.GetSpaceAsync(venueId, spaceId);
        }
        public async Task<List<SpaceDto>> GetSpacesAsync(Guid venueId)
        {
            return await _venueRepo.GetSpacesAsync(venueId);
        }

        public async Task<SpaceDto> EditSpaceAsync(Space space, Guid venueId, Guid spaceId)
        {
            return await _venueRepo.EditSpaceAsync(space, venueId, spaceId);
        }

        public async Task<bool> DeleteSpaceAsync(Guid venueId, Guid spaceId)
        {
            return await _venueRepo.DeleteSpaceAsync(venueId, spaceId);
        }

        public async Task<List<SpaceImageDto>> AddSpaceImagesAsync(List<string> base64EncodedVenueImages, Guid venueId, Guid spaceId)
        {
            return await _venueRepo.AddSpaceImagesAsync(base64EncodedVenueImages, venueId, spaceId);
        }
        public async Task<List<SpaceImageDto>> GetSpaceImagesAsync(Guid venueId, Guid spaceId)
        {
            return await _venueRepo.GetSpaceImagesAsync(venueId, spaceId);
        }
        public async Task<bool> DeleteSpaceImagesAsync(List<Guid> spaceImageIds, Guid venueId, Guid spaceId)
        {
            return await _venueRepo.DeleteSpaceImagesAsync(spaceImageIds, venueId, spaceId);
        }
    }
}
