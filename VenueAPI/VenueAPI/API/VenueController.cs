using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using VenueAPI.BLL;
using VenueAPI.CustomAttributes;
using VLibraries.APIModels;
using VLibraries.ResponseModels;
using VLibraries.VenueAPI;

namespace VenueAPI.API
{
    [Route("Venues")]
    public class VenueController : Controller, IVenueService
    {
        private readonly IVenueProvider _venueProvider;

        public VenueController(IVenueProvider venueProvider)
        {
            _venueProvider = venueProvider;
        }

        /// <summary>
        /// Add a Venue
        /// </summary>
        /// <param name="venue"></param>
        /// <returns></returns>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotModified)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPost]
        [Route("")]
        [CustomModelStateValidation]
        public async Task<ActionResult<ResponseBase<VenueResponse>>> AddVenueAsync([FromBody] VenueRequest venue)
        {
            VenueResponse result = await _venueProvider.AddVenueAsync(venue);

            return new ResponseBase<VenueResponse>(result);
        }

        /// <summary>
        /// Get a Venue by its Id
        /// </summary>
        /// <param name="venueId"></param>
        /// <returns></returns>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("{venueId}")]
        [CustomModelStateValidation]
        public async Task<ActionResult<ResponseBase<VenueResponse>>> GetVenueAsync(Guid venueId)
        {
            VenueResponse result = await _venueProvider.GetVenueAsync(venueId);

            return new ResponseBase<VenueResponse>(result);
        }

        /// <summary>
        /// Get all Venues
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("")]
        [CustomModelStateValidation]
        public async Task<ActionResult<ResponseBase<List<VenueDto>>>> GetVenuesAsync()
        {
            List<VenueDto> result = await _venueProvider.GetVenuesAsync();

            return new ResponseBase<List<VenueDto>>(result);
        }

        /// <summary>
        /// Edit a Venue by its Id
        /// </summary>
        /// <param name="venue"></param>
        /// <param name="venueId"></param>
        /// <returns></returns>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NotModified)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPatch]
        [Route("{venueId}")]
        [CustomModelStateValidation]
        public async Task<ActionResult<ResponseBase<VenueResponse>>> EditVenueAsync([FromBody] VenueRequest venue, Guid venueId)
        {
            VenueResponse result = await _venueProvider.EditVenueAsync(venue, venueId);

            return new ResponseBase<VenueResponse>(result);
        }

        /// <summary>
        /// Delete a Venue by its Id
        /// </summary>
        /// <param name="venueId"></param>
        /// <returns></returns>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NotModified)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpDelete]
        [Route("")]
        [CustomModelStateValidation]
        public async Task<ActionResult<ResponseBase<bool>>> DeleteVenueAsync(Guid venueId)
        {
            bool result = await _venueProvider.DeleteVenueAsync(venueId);

            return new ResponseBase<bool>(result);
        }        
    }
}