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
    [Route("Venues/{venueId}/Spaces")]
    public class SpaceController : Controller, ISpaceService
    {
        private readonly ISpaceProvider _spaceProvider;

        public SpaceController(ISpaceProvider spaceProvider)
        {
            _spaceProvider = spaceProvider;
        }

        /// <summary>
        /// Add a Space to a Venue by the venueId
        /// </summary>
        /// <param name="space"></param>
        /// <param name="venueId"></param>
        /// <returns></returns>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ResponseBase<SpaceResponse>))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NotModified)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPost]
        [Route("")]
        [CustomModelStateValidation]
        public async Task<ActionResult<ResponseBase<SpaceResponse>>> AddSpaceAsync([FromBody] SpaceRequest space, Guid venueId)
        {
            SpaceResponse result = await _spaceProvider.AddSpaceAsync(space, venueId);

            return new ResponseBase<SpaceResponse>(result);
        }

        /// <summary>
        /// Get a Space wtihin a Venue by its spaceId and by the venuId
        /// </summary>
        /// /// <param name="spaceId"></param>
        /// <param name="venueId"></param>
        /// <returns></returns>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ResponseBase<SpaceResponse>))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("{spaceId}")]
        [CustomModelStateValidation]
        public async Task<ActionResult<ResponseBase<SpaceResponse>>> GetSpaceAsync(Guid venueId, Guid spaceId)
        {
            SpaceResponse result = await _spaceProvider.GetSpaceAsync(venueId, spaceId);

            return new ResponseBase<SpaceResponse>(result);
        }

        /// <summary>
        /// Get all Spaces within a Venue by the venueId
        /// </summary>
        /// <param name="venueId"></param>
        /// <returns></returns>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ResponseBase<List<SpaceResponse>>))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("")]
        [CustomModelStateValidation]
        public async Task<ActionResult<ResponseBase<List<SpaceResponse>>>> GetSpacesAsync(Guid venueId)
        {
            List<SpaceResponse> results = await _spaceProvider.GetSpacesAsync(venueId);

            return new ResponseBase<List<SpaceResponse>>(results);
        }

        /// <summary>
        /// Edit a Space within a Venue by using the spaceId and venudId
        /// </summary>
        /// <param name="space"></param>
        /// <param name="spaceId"></param>
        /// <param name="venueId"></param>
        /// <returns></returns>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ResponseBase<SpaceResponse>))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPatch]
        [Route("{spaceId}")]
        public async Task<ActionResult<ResponseBase<SpaceResponse>>> EditSpaceAsync([FromBody] SpaceRequest space, Guid venueId, Guid spaceId)
        {
            SpaceResponse result = await _spaceProvider.EditSpaceAsync(space, venueId, spaceId);

            return new ResponseBase<SpaceResponse>(result);
        }

        /// <summary>
        /// Delete a Space within a Venue by using the spaceId and venudId
        /// </summary>
        /// <param name="spaceId"></param>
        /// <param name="venueId"></param>
        /// <returns></returns>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ResponseBase<bool>))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpDelete]
        [Route("{spaceId}")]
        public async Task<ActionResult<ResponseBase<bool>>> DeleteSpaceAsync(Guid venueId, Guid spaceId)
        {
            bool result = await _spaceProvider.DeleteSpaceAsync(venueId, spaceId);

            return new ResponseBase<bool>(result);
        }
    }
}