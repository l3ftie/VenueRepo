using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VLibraries.APIModels;
using VLibraries.ResponseModels;

namespace VLibraries.VenueService
{
    public interface IVenueService
    {
        Task<ActionResult<ResponseBase<Venue>>> AddVenueAsync([FromBody] Venue venue);
    }
}
