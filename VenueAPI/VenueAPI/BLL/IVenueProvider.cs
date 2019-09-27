using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VLibraries.APIModels;

namespace VenueAPI.BLL
{
    public interface IVenueProvider
    {
        Task<VenueDto> AddVenueAsync(VenueRequest venue);
        Task<VenueDto> GetVenueAsync(Guid venueId);
        Task<List<VenueDto>> GetVenuesAsync();
        Task<VenueDto> EditVenueAsync(VenueRequest venue, Guid venueId);
        Task<bool> DeleteVenueAsync(Guid venueId);

        Task<VenueDto> AddSpaceAsync(SpaceRequest venue, Guid venueId);
    }
}
