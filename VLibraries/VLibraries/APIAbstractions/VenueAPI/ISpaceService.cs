using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VLibraries.APIModels;
using VLibraries.ResponseModels;

namespace VLibraries.VenueAPI
{
    public interface ISpaceService
    {
        Task<ActionResult<ResponseBase<SpaceResponse>>> AddSpaceAsync([FromBody] SpaceRequest space, Guid venueId);
        Task<ActionResult<ResponseBase<SpaceResponse>>> GetSpaceAsync(Guid venueId, Guid spaceId);
        Task<ActionResult<ResponseBase<List<SpaceResponse>>>> GetSpacesAsync(Guid venueId);
        Task<ActionResult<ResponseBase<SpaceResponse>>> EditSpaceAsync([FromBody] SpaceRequest space, Guid venueId, Guid spaceId);
        Task<ActionResult<ResponseBase<bool>>> DeleteSpaceAsync(Guid venueId, Guid spaceId);
    }
}
