using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VLibraries.APIModels;

namespace VenueAPI.DAL
{
    public interface IVenueImageRepository
    {
        Task<int> AddVenueImagesAsync(List<VenueImageDto> venueImageDtos);
        Task<List<VenueImageDto>> GetVenueImagesAsync(Guid venueId, bool requestSpecificallyForVenueImages = true);
        Task<bool> DeleteVenueImagesAsync(List<VenueImageDto> venueImageDtos);

        //Not exposed at API Level
        //Task<List<VenueImageDto>> GetVenueImagesAsync(List<Guid> venueId, bool requestSpecificallyForVenueImages = true); //Not exposed at API Level
    }
}
