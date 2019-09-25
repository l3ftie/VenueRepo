using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace AgentAPI.DAL
{
    public class AgentRepository : IAgentRepository
    {
        private readonly string _connectionString;

        public AgentRepository(IConfiguration config)
        {
            _connectionString = config.GetValue<string>("ConnectionString:LocalSqlServer");
        }

        public async Task<bool> AddAgentAsync()
        {
            return true;
        }
    }
}
