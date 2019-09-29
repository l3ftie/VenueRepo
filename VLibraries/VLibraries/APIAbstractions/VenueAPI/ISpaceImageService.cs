using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VLibraries.APIModels;
using VLibraries.ResponseModels;

namespace VLibraries.VenueAPI
{
    public interface ISpaceImageService
    {
        Task<ActionResult<ResponseBase<List<SpaceImageDto>>>> AddSpaceImagesAsync(List<string> base64EncodedVenueImages, Guid venueId, Guid spaceId);
        Task<ActionResult<ResponseBase<List<SpaceImageDto>>>> GetSpaceImagesAsync(Guid venueId, Guid spaceId);
        Task<ActionResult<ResponseBase<bool>>> DeleteSpaceImagesAsync(List<Guid> spaceImageIds, Guid venueId, Guid spaceId);
    }
}
