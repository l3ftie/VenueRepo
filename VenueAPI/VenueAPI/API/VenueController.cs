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


        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NotModified)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPost]
        [Route("{venueId}")]
        [CustomModelStateValidation]
        public async Task<ActionResult<ResponseBase<SpaceDto>>> AddSpaceAsync([FromBody] Space space, Guid venueId)
        {
            SpaceDto result = await _venueProvider.AddSpaceAsync(space, venueId);

            return new ResponseBase<SpaceDto>(result);
        }

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("{spaceId}")]
        [CustomModelStateValidation]
        public async Task<ActionResult<ResponseBase<SpaceDto>>> GetSpaceAsync(Guid spaceId)
        {
            SpaceDto result = await _venueProvider.GetSpaceAsync(spaceId);

            return new ResponseBase<SpaceDto>(result);
        }

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

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPatch]
        [Route("{venueId}/Spaces/{spaceId}")]
        public async Task<ActionResult<ResponseBase<SpaceDto>>> EditSpaceAsync([FromBody] Space venue, Guid spaceId, Guid venueId)
        {
            SpaceDto result = await _venueProvider.EditSpaceAsync(venue, spaceId, venueId);

            return new ResponseBase<SpaceDto>(result);
        }

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("{venueId}/Spaces/{spaceId}")]
        public async Task<ActionResult<ResponseBase<bool>>> DeleteSpaceAsync(Guid spaceId, Guid venueId)
        {
            bool result = await _venueProvider.DeleteSpaceAsync(spaceId, venueId);

            return new ResponseBase<bool>(result);
        }


        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NotModified)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPost]
        [Route("Images/{spaceId}")]
        [CustomModelStateValidation]
        public async Task<ActionResult<ResponseBase<List<SpaceImageDto>>>> AddSpaceImagesAsync(List<SpaceImage> spaceImages, Guid spaceId)
        {
            List<SpaceImageDto> results = await _venueProvider.AddSpaceImagesAsync(spaceImages, spaceId);

            return new ResponseBase<List<SpaceImageDto>>(results);
        }

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("Images/{spaceId}")]
        [CustomModelStateValidation]
        public async Task<ActionResult<ResponseBase<List<SpaceImageDto>>>> GetSpaceImagesAsync(Guid spaceId)
        {
            List<SpaceImageDto> results = await _venueProvider.GetSpaceImagesAsync(spaceId);

            return new ResponseBase<List<SpaceImageDto>>(results);
        }

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NotModified)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpDelete]
        [Route("Images/{spaceId}")]
        [CustomModelStateValidation]
        public async Task<ActionResult<ResponseBase<bool>>> DeleteSpaceImagesAsync(List<Guid> spaceImageIds, Guid spaceId)
        {
            bool result = await _venueProvider.DeleteSpaceImagesAsync(spaceImageIds, spaceId);

            return new ResponseBase<bool>(result);
        }
    }
}