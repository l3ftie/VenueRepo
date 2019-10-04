using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VLibraries.APIModels;

namespace VenueAPI.DAL
{
    public interface ISpaceRepository
    {
        Task<Guid> AddSpaceAsync(SpaceRequest space, Guid venueId);
        Task<List<SpaceDto>> GetSpaceAsync(Guid venueId, Guid spaceId, bool requestSpecificallyForSpaces = true);
        Task<List<SpaceDto>> GetSpacesAsync(Guid venueId, bool requestSpecificallyForSpaces = true);
        Task<bool> EditSpaceAsync(SpaceRequest venue, Guid venueId, Guid spaceId);
        Task<bool> DeleteSpaceAsync(Guid venueId, Guid spaceId);

        //Not exposed at API Level
        Task<List<SpaceDto>> GetSpacesAsync(List<Guid> venueIds, bool requestSpecificallyForSpaces = true); 
    }
}
