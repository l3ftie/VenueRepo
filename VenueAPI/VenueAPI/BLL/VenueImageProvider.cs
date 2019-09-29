using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VenueAPI.DAL;
using VLibraries.APIModels;

namespace VenueAPI.BLL
{
    class VenueImageProvider : IVenueImageProvider
    {
        private readonly IVenueImageRepository _venueImageRepo;
        public VenueImageProvider(IVenueImageRepository venueImageRepo)
        {
            _venueImageRepo = venueImageRepo;
        }

        public async Task<List<VenueImageDto>> AddVenueImagesAsync(List<string> base64EncodedVenueImages, Guid venueId)
        {
            return await _venueImageRepo.AddVenueImagesAsync(base64EncodedVenueImages, venueId);
        }

        public async Task<List<VenueImageDto>> GetVenueImagesAsync(Guid venueId)
        {
            return await _venueImageRepo.GetVenueImagesAsync(venueId);
        }

        public async Task<bool> DeleteVenueImagesAsync(List<Guid> venueImageIds, Guid venueId)
        {
            return await _venueImageRepo.DeleteVenueImagesAsync(venueImageIds, venueId);
        }
    }
}
