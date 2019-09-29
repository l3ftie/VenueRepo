using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VLibraries.APIModels;
using VLibraries.ResponseModels;

namespace VLibraries.VenueService
{
    public interface IVenueService
    {
        Task<ActionResult<ResponseBase<VenueDto>>> AddVenueAsync([FromBody] Venue venue);
        Task<ActionResult<ResponseBase<VenueDto>>> GetVenueAsync(Guid venueId);
        Task<ActionResult<ResponseBase<List<VenueDto>>>> GetVenuesAsync();
        Task<ActionResult<ResponseBase<VenueDto>>> EditVenueAsync([FromBody] Venue venue, Guid venueId);
        Task<ActionResult<ResponseBase<bool>>> DeleteVenueAsync(Guid venueId);        

        Task<ActionResult<ResponseBase<SpaceDto>>> AddSpaceAsync([FromBody] Space venue, Guid venueId);
        Task<ActionResult<ResponseBase<SpaceDto>>> GetSpaceAsync(Guid spaceId);
        Task<ActionResult<ResponseBase<List<SpaceDto>>>> GetSpacesAsync(Guid venueId);
        Task<ActionResult<ResponseBase<SpaceDto>>> EditSpaceAsync([FromBody] Space venue, Guid spaceId, Guid venueId);
        Task<ActionResult<ResponseBase<bool>>> DeleteSpaceAsync(Guid spaceId, Guid venueId);

        Task<ActionResult<ResponseBase<List<SpaceImageDto>>>> AddSpaceImagesAsync(List<SpaceImage> spaceImages, Guid spaceId);
        Task<ActionResult<ResponseBase<List<SpaceImageDto>>>> GetSpaceImagesAsync(Guid spaceId);
        Task<ActionResult<ResponseBase<bool>>> DeleteSpaceImagesAsync(List<Guid> spaceImageIds, Guid spaceId);
    }
}
