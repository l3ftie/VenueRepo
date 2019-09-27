using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VLibraries.APIModels;

namespace VenueAPI.DAL
{
    public interface IVenueRepository
    {
        Task<VenueDto> AddVenueAsync(VenueRequest venue);
        Task<VenueDto> GetVenueAsync(Guid id);
        Task<List<VenueDto>> GetVenuesAsync();
        Task<VenueDto> EditVenueAsync(VenueRequest venue, Guid id);
        Task<bool> DeleteVenueAsync(Guid id);

        Task<VenueDto> AddSpaceAsync(SpaceRequest space, Guid venueId);

    }
}
