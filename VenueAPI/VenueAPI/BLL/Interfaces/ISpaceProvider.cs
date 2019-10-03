using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VLibraries.APIModels;

namespace VenueAPI.BLL
{
    public interface ISpaceProvider
    {
        Task<SpaceResponse> AddSpaceAsync(SpaceRequest venue, Guid venueId);
        Task<SpaceResponse> GetSpaceAsync(Guid venueId, Guid spaceId);
        Task<List<SpaceResponse>> GetSpacesAsync(Guid venueId);
        Task<SpaceResponse> EditSpaceAsync(SpaceRequest space, Guid venueId, Guid spaceId);
        Task<bool> DeleteSpaceAsync(Guid venueId, Guid spaceId);
        Task<SpaceResponse> UpsertSpaceType(Guid venueId, Guid spaceId, SpaceTypeDto spaceType);
    }
}
