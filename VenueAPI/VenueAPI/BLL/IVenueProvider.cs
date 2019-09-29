using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VLibraries.APIModels;

namespace VenueAPI.BLL
{
    public interface IVenueProvider
    {
        Task<VenueDto> AddVenueAsync(Venue venue);
        Task<VenueDto> GetVenueAsync(Guid venueId);
        Task<List<VenueDto>> GetVenuesAsync();
        Task<VenueDto> EditVenueAsync(Venue venue, Guid venueId);
        Task<bool> DeleteVenueAsync(Guid venueId);

        Task<SpaceDto> AddSpaceAsync(Space venue, Guid venueId);
        Task<SpaceDto> GetSpaceAsync(Guid spaceId);
        Task<List<SpaceDto>> GetSpacesAsync(Guid venueId);
        Task<SpaceDto> EditSpaceAsync(Space space, Guid spaceId, Guid venueId);
        Task<bool> DeleteSpaceAsync(Guid spaceId, Guid venueId);

        Task<List<SpaceImageDto>> AddSpaceImagesAsync(List<SpaceImage> spaceImages, Guid spaceId);
        Task<List<SpaceImageDto>> GetSpaceImagesAsync(Guid spaceId);
        Task<bool> DeleteSpaceImagesAsync(List<Guid> spaceImageIds, Guid spaceId);
    }
}
