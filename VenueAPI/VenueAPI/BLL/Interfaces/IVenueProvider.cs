using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VLibraries.APIModels;

namespace VenueAPI.BLL
{

    public interface IVenueProvider
    {
        Task<VenueResponse> AddVenueAsync(VenueRequest venue);
        Task<VenueResponse> GetVenueAsync(Guid venueId);
        Task<List<VenueResponse>> GetVenuesAsync();
        Task<VenueResponse> EditVenueAsync(VenueRequest venue, Guid venueId);
        Task<bool> DeleteVenueAsync(Guid venueId);     
    }
}
