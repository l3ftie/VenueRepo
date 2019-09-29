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
    [Route("Venues/{venueId}/Spaces/{spaceId}/Images")]
    public class SpaceImageController : Controller, ISpaceImageService
    {
        private readonly ISpaceImageProvider _spaceImageProvider;

        public SpaceImageController(ISpaceImageProvider spaceImageProvider)
        {
            _spaceImageProvider = spaceImageProvider;
        }

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
        [Route("")]
        [CustomModelStateValidation]
        public async Task<ActionResult<ResponseBase<List<SpaceImageDto>>>> AddSpaceImagesAsync(List<string> base64EncodedVenueImages, Guid venueId, Guid spaceId)
        {
            List<SpaceImageDto> results = await _spaceImageProvider.AddSpaceImagesAsync(base64EncodedVenueImages, venueId, spaceId);

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
        [Route("")]
        [CustomModelStateValidation]
        public async Task<ActionResult<ResponseBase<List<SpaceImageDto>>>> GetSpaceImagesAsync(Guid venueId, Guid spaceId)
        {
            List<SpaceImageDto> results = await _spaceImageProvider.GetSpaceImagesAsync(venueId, spaceId);

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
        [Route("")]
        [CustomModelStateValidation]
        public async Task<ActionResult<ResponseBase<bool>>> DeleteSpaceImagesAsync(List<Guid> spaceImageIds, Guid venueId, Guid spaceId)
        {
            bool result = await _spaceImageProvider.DeleteSpaceImagesAsync(spaceImageIds, venueId, spaceId);

            return new ResponseBase<bool>(result);
        }
    }
}