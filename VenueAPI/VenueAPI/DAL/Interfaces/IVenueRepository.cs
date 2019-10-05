using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VLibraries.APIModels;

namespace VenueAPI.DAL
{

    public interface IVenueRepository
    {
        Task<Guid> AddVenueAsync(VenueDto venue);
        Task<List<VenueDto>> GetVenueAsync(Guid id);
        Task<List<VenueDto>> GetVenuesAsync();
        Task<bool> EditVenueAsync(VenueDto venue);
        Task<bool> DeleteVenueAsync(Guid id);
    }
}
