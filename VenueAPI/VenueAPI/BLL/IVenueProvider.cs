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

        Task<List<VenueImageDto>> AddVenueImagesAsync(List<string> base64EncodedVenueImages, Guid venueId);
        Task<List<VenueImageDto>> GetVenueImagesAsync(Guid venueId);
        Task<bool> DeleteVenueImagesAsync(List<Guid> venueImageIds, Guid venueId);
      

        Task<SpaceDto> AddSpaceAsync(Space venue, Guid venueId);
        Task<SpaceDto> GetSpaceAsync(Guid venueId, Guid spaceId);
        Task<List<SpaceDto>> GetSpacesAsync(Guid venueId);
        Task<SpaceDto> EditSpaceAsync(Space space, Guid venueId, Guid spaceId);
        Task<bool> DeleteSpaceAsync(Guid venueId, Guid spaceId);

        Task<List<SpaceImageDto>> AddSpaceImagesAsync(List<string> base64EncodedVenueImages, Guid venueId, Guid spaceId);
        Task<List<SpaceImageDto>> GetSpaceImagesAsync(Guid venueId, Guid spaceId);
        Task<bool> DeleteSpaceImagesAsync(List<Guid> spaceImageIds, Guid venueId, Guid spaceId);
    }
}
