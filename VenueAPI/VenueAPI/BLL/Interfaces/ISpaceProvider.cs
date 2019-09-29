using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VLibraries.APIModels;

namespace VenueAPI.BLL
{
    public interface ISpaceProvider
    {
        Task<SpaceDto> AddSpaceAsync(Space venue, Guid venueId);
        Task<SpaceDto> GetSpaceAsync(Guid venueId, Guid spaceId);
        Task<List<SpaceDto>> GetSpacesAsync(Guid venueId);
        Task<SpaceDto> EditSpaceAsync(Space space, Guid venueId, Guid spaceId);
        Task<bool> DeleteSpaceAsync(Guid venueId, Guid spaceId);
    }
}
