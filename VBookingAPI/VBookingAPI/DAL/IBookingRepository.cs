using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingAPI.DAL
{
    public interface IVBookingRepository
    {
        Task<bool> AddBookingAsync();
    }
}
