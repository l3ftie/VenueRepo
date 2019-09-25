using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using BookingAPI.BLL;
using VLibraries.CustomExceptions;
using VLibraries.ResponseModels;
using VLibraries.BookingService;

namespace BookingAPI.API
{
    [Route("Bookings")]
    public class BookingController : Controller, IBookingService
    {
        private readonly IBookingProvider _bookingProvider;

        public BookingController(IBookingProvider bookingProvider)
        {
            _bookingProvider = bookingProvider;
        }

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<ResponseBase<bool>>> AddBookingAsync()
        {
            if (!ModelState.IsValid)
                throw new HttpStatusCodeResponseException(HttpStatusCode.BadRequest);

            bool result = await _bookingProvider.AddBookingAsync();

            return new ResponseBase<bool>(result);
        }
    }
}