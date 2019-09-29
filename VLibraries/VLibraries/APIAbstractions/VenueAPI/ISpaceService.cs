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
        Task<ActionResult<ResponseBase<SpaceDto>>> AddSpaceAsync([FromBody] Space space, Guid venueId);
        Task<ActionResult<ResponseBase<SpaceDto>>> GetSpaceAsync(Guid venueId, Guid spaceId);
        Task<ActionResult<ResponseBase<List<SpaceDto>>>> GetSpacesAsync(Guid venueId);
        Task<ActionResult<ResponseBase<SpaceDto>>> EditSpaceAsync([FromBody] Space space, Guid venueId, Guid spaceId);
        Task<ActionResult<ResponseBase<bool>>> DeleteSpaceAsync(Guid venueId, Guid spaceId);
    }
}
