using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VLibraries.APIModels;

namespace VenueAPI.DAL
{
    public interface ISpaceRepository
    {
        Task<SpaceResponse> AddSpaceAsync(SpaceRequest space, Guid venueId);
        Task<SpaceResponse> GetSpaceAsync(Guid venueId, Guid spaceId, bool requestSpecificallyForSpaces = true);
        Task<List<SpaceResponse>> GetSpacesAsync(Guid venueId, bool requestSpecificallyForSpaces = true);
        Task<SpaceResponse> EditSpaceAsync(SpaceRequest venue, Guid venueId, Guid spaceId);
        Task<bool> DeleteSpaceAsync(Guid venueId, Guid spaceId);

        Task<List<SpaceResponse>> GetSpacesAsync(List<Guid> venueIds, bool requestSpecificallyForSpaces = true); //Not exposed at API Level
    }
}
