using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VLibraries.APIModels;

namespace VenueAPI.DAL
{
    public interface IVenueRepository
    {
        Task<Venue> AddVenueAsync(Venue venue);
    }
}
