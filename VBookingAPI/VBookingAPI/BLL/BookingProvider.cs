using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using BookingAPI.DAL;

namespace BookingAPI.BLL
{
    class BookingProvider : IBookingProvider
    {
        private readonly IVBookingRepository _bookingRepo;
        public BookingProvider(IVBookingRepository bookingRepo)
        {
            _bookingRepo = bookingRepo;
        }

        public async Task<bool> AddBookingAsync()
        {
            return await _bookingRepo.AddBookingAsync();
        }
    }
}
