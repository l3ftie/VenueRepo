using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VLibraries.APIModels;

namespace VenueAPI.Extensions
{
    public static class ImageMappingExtensions
    {
        public static List<VenueImageDto> MapVenueImageStringsToDtos(this List<string> base64EncodedVenueImages, Guid venueId)
        {
            List<VenueImageDto> venueImageDtos = new List<VenueImageDto>();

            base64EncodedVenueImages.ForEach(imgString => venueImageDtos.Add(new VenueImageDto
            {
                Base64VenueImageString = imgString,
                VenueId = venueId
            }));

            return venueImageDtos;
        }


        public static List<SpaceImageDto> MapSpaceImageStringsToDtos(this List<string> base64EncodedVenueImages, Guid spaceId)
        {
            List<SpaceImageDto> spaceImageDtos = new List<SpaceImageDto>();

            base64EncodedVenueImages.ForEach(imgString => spaceImageDtos.Add(new SpaceImageDto
            {
                Base64SpaceImageString = imgString,
                SpaceId = spaceId
            }));

            return spaceImageDtos;
        }

        public static List<VenueImageDto> MapVenueIdDetailsToDtos(this List<Guid> venueImageIds, Guid venueId)
        {
            List<VenueImageDto> venueImageDtos = new List<VenueImageDto>();

            venueImageIds.ForEach(x => venueImageDtos.Add(new VenueImageDto { VenueId = venueId, VenueImageId = x }));

            return venueImageDtos;
        }
               
        public static List<SpaceImageDto> MapSpaceIdDetailsToDtos(this List<Guid> spaceImageIds, Guid spaceId)
        {
            List<SpaceImageDto> spaceImageDtos = new List<SpaceImageDto>();

            spaceImageIds.ForEach(x => spaceImageDtos.Add(new SpaceImageDto { SpaceId = spaceId, SpaceImageId = x }));

            return spaceImageDtos;
        }
    }
}
