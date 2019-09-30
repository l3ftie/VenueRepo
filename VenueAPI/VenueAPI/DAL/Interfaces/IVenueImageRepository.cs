using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VLibraries.APIModels;

namespace VenueAPI.DAL
{
    public interface IVenueImageRepository
    {
        Task<List<VenueImageDto>> AddVenueImagesAsync(List<string> base64EncodedVenueImages, Guid venueId);
        Task<List<VenueImageDto>> GetVenueImagesAsync(Guid venueId, bool requestSpecificallyForVenueImages = true);
        Task<bool> DeleteVenueImagesAsync(List<Guid> venueImageIds, Guid venueId);


        Task<List<VenueImageDto>> GetVenueImagesAsync(List<Guid> venueId, bool requestSpecificallyForVenueImages = true); //Not exposed at API Level
    }
}
