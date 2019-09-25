using System.Threading.Tasks;

namespace BookingAPI.BLL
{
    public interface IBookingProvider
    {
        Task<bool> AddBookingAsync();
    }
}
