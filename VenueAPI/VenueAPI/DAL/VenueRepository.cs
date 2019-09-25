using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace VenueAPI.DAL
{
    public class VenueRepository : IVenueRepository
    {
        private readonly string _connectionString;

        public VenueRepository(IConfiguration config)
        {
            _connectionString = config.GetValue<string>("ConnectionString:LocalSqlServer");
        }

        public async Task<bool> AddVenueAsync()
        {
            return true;
        }
    }
}
