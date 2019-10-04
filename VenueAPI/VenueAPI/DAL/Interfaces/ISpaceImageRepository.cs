using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VLibraries.APIModels;

namespace VenueAPI.DAL
{
    public interface ISpaceImageRepository
    {
        Task<int> AddSpaceImagesAsync(List<SpaceImageDto> spaceImageDtos);
        Task<List<SpaceImageDto>> GetSpaceImagesAsync(Guid venueId, Guid spaceId, bool requestSpecificallyForSpaceImages = true);
        Task<bool> DeleteSpaceImagesAsync(List<SpaceImageDto> spaceImageDtos);

        //Not exposed at API Level
        Task<List<SpaceImageDto>> GetSpaceImagesAsync(List<Guid> spaceIds, bool requestSpecificallyForSpaceImages = true); 
    }
}
