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
    [Route("Venues/{venueId}/Images")]
    public class VenueImageController : Controller, IVenueImageService
    {
        private readonly IVenueImageProvider _venueImageProvider;

        public VenueImageController(IVenueImageProvider venueImageProvider)
        {
            _venueImageProvider = venueImageProvider;
        }

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
        [Route("")]
        [CustomModelStateValidation]
        public async Task<ActionResult<ResponseBase<List<VenueImageDto>>>> AddVenueImagesAsync(List<string> base64EncodedVenueImages, Guid venueId)
        {
            List<VenueImageDto> results = await _venueImageProvider.AddVenueImagesAsync(base64EncodedVenueImages, venueId);

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
        [Route("")]
        [CustomModelStateValidation]
        public async Task<ActionResult<ResponseBase<List<VenueImageDto>>>> GetVenueImagesAsync(Guid venueId)
        {
            List<VenueImageDto> results = await _venueImageProvider.GetVenueImagesAsync(venueId);

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
        [Route("")]
        [CustomModelStateValidation]
        public async Task<ActionResult<ResponseBase<bool>>> DeleteVenueImagesAsync(List<Guid> venueImageIds, Guid venueId)
        {
            bool result = await _venueImageProvider.DeleteVenueImagesAsync(venueImageIds, venueId);

            return new ResponseBase<bool>(result);
        }
    }
}