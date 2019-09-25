using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VLibraries.ResponseModels;

namespace VLibraries.AgentService
{
    public interface IAgentService
    {
        Task<ActionResult<ResponseBase<bool>>> AddAgentAsync();
    }
}
