using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VLibraries.APIModels;

namespace VenueAPI.DAL
{
    public interface IVenueRepository
    {
        Task<VenueDto> AddVenueAsync(Venue venue);
        Task<VenueDto> GetVenueAsync(Guid id);
        Task<List<VenueDto>> GetVenuesAsync();
        Task<VenueDto> EditVenueAsync(Venue venue, Guid id);
        Task<bool> DeleteVenueAsync(Guid id);

        Task<VenueDto> AddSpaceAsync(Space space, Guid venueId);

    }
}
