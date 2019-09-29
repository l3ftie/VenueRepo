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

        Task<ActionResult<ResponseBase<List<VenueImageDto>>>> AddVenueImagesAsync(List<string> base64EncodedVenueImages, Guid venueId);
        Task<ActionResult<ResponseBase<List<VenueImageDto>>>> GetVenueImagesAsync(Guid venueId);
        Task<ActionResult<ResponseBase<bool>>> DeleteVenueImagesAsync(List<Guid> venueImageIds, Guid venueId);


        Task<ActionResult<ResponseBase<SpaceDto>>> AddSpaceAsync([FromBody] Space space, Guid venueId);
        Task<ActionResult<ResponseBase<SpaceDto>>> GetSpaceAsync(Guid venueId, Guid spaceId);
        Task<ActionResult<ResponseBase<List<SpaceDto>>>> GetSpacesAsync(Guid venueId);
        Task<ActionResult<ResponseBase<SpaceDto>>> EditSpaceAsync([FromBody] Space space, Guid venueId, Guid spaceId);
        Task<ActionResult<ResponseBase<bool>>> DeleteSpaceAsync(Guid venueId, Guid spaceId);
        
        Task<ActionResult<ResponseBase<List<SpaceImageDto>>>> AddSpaceImagesAsync(List<string> base64EncodedVenueImages, Guid venueId, Guid spaceId);
        Task<ActionResult<ResponseBase<List<SpaceImageDto>>>> GetSpaceImagesAsync(Guid venueId, Guid spaceId);
        Task<ActionResult<ResponseBase<bool>>> DeleteSpaceImagesAsync(List<Guid> spaceImageIds, Guid venueId, Guid spaceId);
    }
}
