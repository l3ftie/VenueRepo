using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VLibraries.APIModels;

namespace VenueAPI.DAL
{
    public interface ISpaceRepository
    {
        Task<Guid> AddSpaceAsync(SpaceDto spaceDto);
        Task<List<SpaceDto>> GetSpaceAsync(Guid venueId, Guid spaceId, bool requestSpecificallyForSpaces = true);
        Task<List<SpaceDto>> GetSpacesAsync(Guid venueId, bool requestSpecificallyForSpaces = true);
        Task<bool> EditSpaceAsync(SpaceDto spaceDto);
        Task<bool> DeleteSpaceAsync(Guid venueId, Guid spaceId);

        //Not exposed at API Level
        Task<List<SpaceDto>> GetSpacesAsync(List<Guid> venueIds, bool requestSpecificallyForSpaces = true); 
    }
}
