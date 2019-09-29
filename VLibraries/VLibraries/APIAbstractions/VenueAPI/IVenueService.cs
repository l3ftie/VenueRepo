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

        Task<ActionResult<ResponseBase<VenueDto>>> AddSpaceAsync([FromBody] Space venue, Guid venueId);
    }
}
