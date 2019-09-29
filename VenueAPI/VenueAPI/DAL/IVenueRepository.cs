using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VLibraries.APIModels;

namespace VenueAPI.DAL
{
    public interface IVenueRepository
    {
        Task<VenueDto> AddVenueAsync(Venue venue);
        Task<VenueDto> GetVenueAsync(Guid id);
        Task<List<VenueDto>> GetVenuesAsync();
        Task<VenueDto> EditVenueAsync(Venue venue, Guid id);
        Task<bool> DeleteVenueAsync(Guid id);

        Task<SpaceDto> AddSpaceAsync(Space space, Guid venueId);
        Task<SpaceDto> GetSpaceAsync(Guid spaceId);
        Task<List<SpaceDto>> GetSpacesAsync(Guid venueId);
        Task<SpaceDto> EditSpaceAsync(Space venue, Guid spaceId, Guid venueId);
        Task<bool> DeleteSpaceAsync(Guid spaceId, Guid venueId);

        Task<List<SpaceImageDto>> AddSpaceImagesAsync(List<SpaceImage> spaceImages, Guid spaceId);
        Task<List<SpaceImageDto>> GetSpaceImagesAsync(Guid spaceId);
        Task<bool> DeleteSpaceImagesAsync(List<Guid> spaceImageIds, Guid spaceId);

    }
}
