using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VLibraries.APIModels;

namespace VenueAPI.Extensions
{
    public static class SpaceMappingExtensions
    {

        public static SpaceDto MapRequestToDto(this SpaceRequest space, Guid venueId, Guid spaceId)
        {
            return new SpaceDto
            {
                Title = space.Title,
                Description = space.Description,
                Summary = space.Summary,
                MUrl = space.MUrl,
                MaxCapacity = space.MaxCapacity,
                SpaceTypeId = space.SpaceTypeId,
                VenueId = venueId,
                SpaceId = spaceId
            };
        }

        public static SpaceResponse MapDtosToResponse(this IEnumerable<SpaceDto> spaceDtos)
        {
            SpaceDto spaceDto = spaceDtos.FirstOrDefault();

            SpaceResponse model = new SpaceResponse
            {
                SpaceId = spaceDto.SpaceId,
                MaxCapacity = spaceDto.MaxCapacity,
                Title = spaceDto.Title,
                Description = spaceDto.Description,
                Summary = spaceDto.Summary,
                MUrl = spaceDto.MUrl,
                VenueId = spaceDto.VenueId,
                SpaceImages = new List<SpaceImageDto>(),
                SpaceType = new SpaceTypeDto
                {
                    SpaceTypeId = spaceDto.SpaceTypeId,
                    Description = spaceDto.SpaceTypeDescription
                }
            };

            foreach (SpaceDto space in spaceDtos)
            {
                if (space.SpaceImageId != Guid.Empty)
                    model.SpaceImages.Add(new SpaceImageDto
                    {
                        Base64SpaceImageString = space.Base64SpaceImageString,
                        SpaceId = space.SpaceId,
                        SpaceImageId = space.SpaceImageId
                    });

                if (space.SpaceTypeId != Guid.Empty)
                {
                    model.SpaceType.SpaceTypeId = space.SpaceTypeId;
                    model.SpaceType.Description = space.SpaceTypeDescription;
                }
            }

            return model;
        }

        public static List<SpaceResponse> MapDtosToResponses(this List<SpaceDto> spaceDtos)
        {
            List<SpaceResponse> mappedSpaces = new List<SpaceResponse>();

            List<List<SpaceDto>> groupedSpaces = spaceDtos.GroupSpacesBySpaceId();

            foreach (List<SpaceDto> spacesGroupedById in groupedSpaces)
            {
                mappedSpaces.Add(spacesGroupedById.MapDtosToResponse());
            }

            return mappedSpaces.ToList();
        }
    }
}
