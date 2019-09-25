using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BookingAPI.DAL
{
    public class BookingRepository : IVBookingRepository
    {
        private readonly string _connectionString;

        public BookingRepository(IConfiguration config)
        {
            _connectionString = config.GetValue<string>("ConnectionString:LocalSqlServer");
        }

        public async Task<bool> AddBookingAsync()
        {
            return true;
        }
    }
}
