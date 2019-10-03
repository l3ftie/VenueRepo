using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VLibraries.APIModels;

namespace VenueAPI.DAL
{

    public interface IVenueRepository
    {
        Task<VenueResponse> AddVenueAsync(VenueRequest venue);
        Task<VenueResponse> GetVenueAsync(Guid id);
        Task<List<VenueDto>> GetVenuesAsync();
        Task<VenueResponse> EditVenueAsync(VenueRequest venue, Guid id);
        Task<bool> DeleteVenueAsync(Guid id);
    }
}
