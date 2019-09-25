using System.Threading.Tasks;

namespace AgentAPI.BLL
{
    public interface IAgentProvider
    {
        Task<bool> AddAgentAsync();
    }
}
