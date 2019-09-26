using System.Threading.Tasks;
using VLibraries.APIModels;

namespace VenueAPI.BLL
{
    public interface IVenueProvider
    {
        Task<Venue> AddVenueAsync(Venue venue);
    }
}
