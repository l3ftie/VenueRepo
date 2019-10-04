using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VLibraries.APIModels;

namespace VenueAPI.BLL
{
    public interface ISpaceProvider
    {
        Task<SpaceResponse> AddSpaceAsync(SpaceRequest venue, Guid venueId);
        Task<SpaceResponse> GetSpaceAsync(Guid venueId, Guid spaceId, bool requestSpecificallyForSpaces = true);
        Task<List<SpaceResponse>> GetSpacesAsync(Guid venueId, bool requestSpecificallyForSpaces = true);
        Task<SpaceResponse> EditSpaceAsync(SpaceRequest space, Guid venueId, Guid spaceId);
        Task<bool> DeleteSpaceAsync(Guid venueId, Guid spaceId);

        //Not exposed at API Level
        Task<List<SpaceResponse>> GetSpacesAsync(List<Guid> venueIds, bool requestSpecificallyForSpaces = true);
    }
}
