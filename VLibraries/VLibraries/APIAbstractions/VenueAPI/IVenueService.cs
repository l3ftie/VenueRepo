using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VLibraries.ResponseModels;

namespace VLibraries.VenueService
{
    public interface IVenueService
    {
        Task<ActionResult<ResponseBase<bool>>> AddVenueAsync();
    }
}
