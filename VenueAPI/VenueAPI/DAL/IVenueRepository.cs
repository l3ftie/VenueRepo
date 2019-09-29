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
        Task<VenueDto> GetVenueAsync(Guid id, bool initialInsert = false);
        Task<List<VenueDto>> GetVenuesAsync();
        Task<VenueDto> EditVenueAsync(Venue venue, Guid id);
        Task<bool> DeleteVenueAsync(Guid id);

        Task<List<VenueImageDto>> AddVenueImagesAsync(List<string> base64EncodedVenueImages, Guid venueId);
        Task<List<VenueImageDto>> GetVenueImagesAsync(Guid venueId);
        Task<bool> DeleteVenueImagesAsync(List<Guid> venueImageIds, Guid venueId);


        Task<SpaceDto> AddSpaceAsync(Space space, Guid venueId);
        Task<SpaceDto> GetSpaceAsync(Guid venueId, Guid spaceId, bool requestSpecificallyForSpaces = true);
        Task<List<SpaceDto>> GetSpacesAsync(Guid venueId, bool requestSpecificallyForSpaces = true);
        Task<SpaceDto> EditSpaceAsync(Space venue, Guid venueId, Guid spaceId);
        Task<bool> DeleteSpaceAsync(Guid venueId, Guid spaceId);

        Task<List<SpaceImageDto>> AddSpaceImagesAsync(List<string> base64EncodedVenueImages, Guid venueId, Guid spaceId);
        Task<List<SpaceImageDto>> GetSpaceImagesAsync(Guid venueId, Guid spaceId, bool requestSpecificallyForSpaceImages = true);
        Task<bool> DeleteSpaceImagesAsync(List<Guid> spaceImageIds, Guid venueId, Guid spaceId);

    }
}
