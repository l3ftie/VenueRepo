using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VLibraries.APIModels;
using VLibraries.ResponseModels;

namespace VLibraries.VenueAPI
{
    public interface IVenueImageService
    {
        Task<ActionResult<ResponseBase<List<VenueImageDto>>>> AddVenueImagesAsync(List<string> base64EncodedVenueImages, Guid venueId);
        Task<ActionResult<ResponseBase<List<VenueImageDto>>>> GetVenueImagesAsync(Guid venueId);
        Task<ActionResult<ResponseBase<bool>>> DeleteVenueImagesAsync(List<Guid> venueImageIds, Guid venueId);
    }
}
