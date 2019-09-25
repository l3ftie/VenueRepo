using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using AgentAPI.DAL;

namespace AgentAPI.BLL
{
    class AgentProvider : IAgentProvider
    {
        private readonly IAgentRepository _agentRepo;
        public AgentProvider(IAgentRepository VAgentRepo)
        {
            _agentRepo = VAgentRepo;
        }

        public async Task<bool> AddAgentAsync()
        {
            return await _agentRepo.AddAgentAsync();
        }
    }
}
