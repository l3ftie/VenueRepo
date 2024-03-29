﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VLibraries.APIModels;

namespace VenueAPI.BLL
{
    public interface IVenueImageProvider
    {
        Task<List<VenueImageDto>> AddVenueImagesAsync(List<string> base64EncodedVenueImages, Guid venueId);
        Task<List<VenueImageDto>> GetVenueImagesAsync(Guid venueId);
        Task<bool> DeleteVenueImagesAsync(List<Guid> venueImageIds, Guid venueId);
    }
}
