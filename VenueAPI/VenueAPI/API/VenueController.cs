using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using VenueAPI.BLL;
using VLibraries.CustomExceptions;
using VLibraries.ResponseModels;
using VLibraries.VenueService;

namespace VenueAPI.API
{
    [Route("venues")]
    public class VenueController : Controller, IVenueService
    {
        private readonly IVenueProvider _venueProvider;

        public VenueController(IVenueProvider venueProvider)
        {
            _venueProvider = venueProvider;
        }

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<ResponseBase<bool>>> AddVenueAsync()
        {
            if (!ModelState.IsValid)
                throw new HttpStatusCodeResponseException(HttpStatusCode.BadRequest);

            bool result = await _venueProvider.AddVenueAsync();

            return new ResponseBase<bool>(result);
        }
    }
}