using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using VenueAPI.BLL;
using VenueAPI.CustomAttributes;
using VLibraries.APIModels;
using VLibraries.CustomExceptions;
using VLibraries.ResponseModels;
using VLibraries.VenueService;

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


        #region Venues
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
        public async Task<ActionResult<ResponseBase<VenueDto>>> AddVenueAsync([FromBody] Venue venue)
        {
            VenueDto result = await _venueProvider.AddVenueAsync(venue);

            return new ResponseBase<VenueDto>(result);
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
        public async Task<ActionResult<ResponseBase<VenueDto>>> GetVenueAsync(Guid venueId)
        {
            VenueDto result = await _venueProvider.GetVenueAsync(venueId);

            return new ResponseBase<VenueDto>(result);
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
        public async Task<ActionResult<ResponseBase<VenueDto>>> EditVenueAsync([FromBody] Venue venue, Guid venueId)
        {
            VenueDto result = await _venueProvider.EditVenueAsync(venue, venueId);

            return new ResponseBase<VenueDto>(result);
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
        #endregion

        #region Venue Images
        /// <summary>
        /// Add Base64 encoded Image strings to a venue by the venueId
        /// </summary>
        /// <param name="base64EncodedVenueImages"></param>
        /// <param name="venueId"></param>
        /// <returns></returns>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NotModified)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPost]
        [Route("{venueId}/Images")]
        [CustomModelStateValidation]
        public async Task<ActionResult<ResponseBase<List<VenueImageDto>>>> AddVenueImagesAsync(List<string> base64EncodedVenueImages, Guid venueId)
        {
            List<VenueImageDto> results = await _venueProvider.AddVenueImagesAsync(base64EncodedVenueImages, venueId);

            return new ResponseBase<List<VenueImageDto>>(results);
        }

        /// <summary>
        /// Get Base64 encoded Image strings of a Venue by the venueId
        /// </summary>
        /// <param name="venueId"></param>
        /// <returns></returns>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("{venueId}/Images")]
        [CustomModelStateValidation]
        public async Task<ActionResult<ResponseBase<List<VenueImageDto>>>> GetVenueImagesAsync(Guid venueId)
        {
            List<VenueImageDto> results = await _venueProvider.GetVenueImagesAsync(venueId);

            return new ResponseBase<List<VenueImageDto>>(results);
        }

        /// <summary>
        /// Delete Base64 encoded Image strings of a Venue by the venueId
        /// </summary>
        /// <param name="venueId"></param>
        /// <param name="venueImageIds"></param>
        /// <returns></returns>
        /// [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NotModified)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpDelete]
        [Route("{venueId}/Images")]
        [CustomModelStateValidation]
        public async Task<ActionResult<ResponseBase<bool>>> DeleteVenueImagesAsync(List<Guid> venueImageIds, Guid venueId)
        {
            bool result = await _venueProvider.DeleteVenueImagesAsync(venueImageIds, venueId);

            return new ResponseBase<bool>(result);
        }
        #endregion

        #region Spaces
        /// <summary>
        /// Add a Space to a Venue by the venueId
        /// </summary>
        /// <param name="space"></param>
        /// <param name="venueId"></param>
        /// <returns></returns>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NotModified)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPost]
        [Route("{venueId}/Spaces")]
        [CustomModelStateValidation]
        public async Task<ActionResult<ResponseBase<SpaceDto>>> AddSpaceAsync([FromBody] Space space, Guid venueId)
        {
            SpaceDto result = await _venueProvider.AddSpaceAsync(space, venueId);

            return new ResponseBase<SpaceDto>(result);
        }

        /// <summary>
        /// Get a Space wtihin a Venue by its spaceId and by the venuId
        /// </summary>
        /// /// <param name="spaceId"></param>
        /// <param name="venueId"></param>
        /// <returns></returns>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("{venueId}/Spaces/{spaceId}")]
        [CustomModelStateValidation]
        public async Task<ActionResult<ResponseBase<SpaceDto>>> GetSpaceAsync(Guid venueId, Guid spaceId)
        {
            SpaceDto result = await _venueProvider.GetSpaceAsync(venueId, spaceId);

            return new ResponseBase<SpaceDto>(result);
        }

        /// <summary>
        /// Get all Spaces within a Venue by the venueId
        /// </summary>
        /// <param name="venueId"></param>
        /// <returns></returns>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("{venueId}/Spaces")]
        [CustomModelStateValidation]
        public async Task<ActionResult<ResponseBase<List<SpaceDto>>>> GetSpacesAsync(Guid venueId)
        {
            List<SpaceDto> results = await _venueProvider.GetSpacesAsync(venueId);

            return new ResponseBase<List<SpaceDto>> (results);
        }

        /// <summary>
        /// Edit a Space within a Venue by using the spaceId and venudId
        /// </summary>
        /// <param name="space"></param>
        /// <param name="spaceId"></param>
        /// <param name="venueId"></param>
        /// <returns></returns>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPatch]
        [Route("{venueId}/Spaces/{spaceId}")]
        public async Task<ActionResult<ResponseBase<SpaceDto>>> EditSpaceAsync([FromBody] Space space, Guid venueId, Guid spaceId)
        {
            SpaceDto result = await _venueProvider.EditSpaceAsync(space, venueId, spaceId);

            return new ResponseBase<SpaceDto>(result);
        }

        /// <summary>
        /// Delete a Space within a Venue by using the spaceId and venudId
        /// </summary>
        /// <param name="spaceId"></param>
        /// <param name="venueId"></param>
        /// <returns></returns>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpDelete]
        [Route("{venueId}/Spaces/{spaceId}")]
        public async Task<ActionResult<ResponseBase<bool>>> DeleteSpaceAsync(Guid venueId, Guid spaceId)
        {
            bool result = await _venueProvider.DeleteSpaceAsync(venueId, spaceId);

            return new ResponseBase<bool>(result);
        }
        #endregion


        #region Space Images
        /// <summary>
        /// Add Base64 encoded Image strings to a Space within a Venue by the spaceId and venueId
        /// </summary>
        /// <param name="base64EncodedVenueImages"></param>
        /// <param name="venueId"></param>
        /// <param name="spaceId"></param>
        /// <returns></returns>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NotModified)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPost]
        [Route("{venueId}/Spaces/{spaceId}/Images")]
        [CustomModelStateValidation]
        public async Task<ActionResult<ResponseBase<List<SpaceImageDto>>>> AddSpaceImagesAsync(List<string> base64EncodedVenueImages, Guid venueId, Guid spaceId)
        {
            List<SpaceImageDto> results = await _venueProvider.AddSpaceImagesAsync(base64EncodedVenueImages, venueId, spaceId);

            return new ResponseBase<List<SpaceImageDto>>(results);
        }

        /// <summary>
        /// Get Base64 encoded Image strings for a Space within a Venue by the spaceId and the venueId
        /// </summary>
        /// <param name="venueId"></param> 
        /// <param name="spaceId"></param>
        /// <returns></returns>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("{venueId}/Spaces/{spaceId}/Images")]
        [CustomModelStateValidation]
        public async Task<ActionResult<ResponseBase<List<SpaceImageDto>>>> GetSpaceImagesAsync(Guid venueId, Guid spaceId)
        {
            List<SpaceImageDto> results = await _venueProvider.GetSpaceImagesAsync(venueId, spaceId);

            return new ResponseBase<List<SpaceImageDto>>(results);
        }

        /// <summary>
        /// Delete Base64 encoded Image strings for a Space within a Venue by the imageIds, spaceId and venueId
        /// </summary>
        /// <param name="spaceImageIds"></param>
        /// <param name="spaceId"></param>
        /// <param name="venueId"></param>
        /// <returns></returns>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NotModified)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpDelete]
        [Route("{venueId}/Spaces/{spaceId}/Images")]
        [CustomModelStateValidation]
        public async Task<ActionResult<ResponseBase<bool>>> DeleteSpaceImagesAsync(List<Guid> spaceImageIds, Guid venueId, Guid spaceId)
        {
            bool result = await _venueProvider.DeleteSpaceImagesAsync(spaceImageIds, venueId, spaceId);

            return new ResponseBase<bool>(result);
        }
        #endregion
    }
}