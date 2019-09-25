using System.Threading.Tasks;

namespace VenueAPI.BLL
{
    public interface IVenueProvider
    {
        Task<bool> AddVenueAsync();
    }
}
