using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VLibraries.APIModels;
using VLibraries.ResponseModels;

namespace VLibraries.VenueAPI
{
    public interface IVenueService
    {
        Task<ActionResult<ResponseBase<VenueResponse>>> AddVenueAsync([FromBody] VenueRequest venue);
        Task<ActionResult<ResponseBase<VenueResponse>>> GetVenueAsync(Guid venueId);
        Task<ActionResult<ResponseBase<List<VenueResponse>>>> GetVenuesAsync();
        Task<ActionResult<ResponseBase<VenueResponse>>> EditVenueAsync([FromBody] VenueRequest venue, Guid venueId);
        Task<ActionResult<ResponseBase<bool>>> DeleteVenueAsync(Guid venueId);                
    }
}
