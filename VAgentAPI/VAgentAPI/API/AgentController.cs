using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using AgentAPI.BLL;
using VLibraries.CustomExceptions;
using VLibraries.ResponseModels;
using VLibraries.AgentService;

namespace AgentAPI.API
{
    [Route("VAgents")]
    public class AgentController : Controller, IAgentService
    {
        private readonly IAgentProvider _agentProvider;

        public AgentController(IAgentProvider VAgentProvider)
        {
            _agentProvider = VAgentProvider;
        }

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<ResponseBase<bool>>> AddAgentAsync()
        {
            if (!ModelState.IsValid)
                throw new HttpStatusCodeResponseException(HttpStatusCode.BadRequest);

            bool result = await _agentProvider.AddAgentAsync();

            return new ResponseBase<bool>(result);
        }
    }
}