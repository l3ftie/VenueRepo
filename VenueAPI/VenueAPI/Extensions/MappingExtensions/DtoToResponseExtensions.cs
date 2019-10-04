﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VLibraries.APIModels;

namespace VenueAPI.Extensions
{
    public static class DtoToResponseExtensions
    {
        public static List<SpaceResponse> MapDtosToResponses(this List<SpaceDto> spaceDtos)
        {
            List<List<SpaceDto>> groupedSpaces = spaceDtos.GroupSpacesBySpaceId();

            List<SpaceResponse> mappedSpaces = new List<SpaceResponse>();

            foreach (List<SpaceDto> spacesGroupedById in groupedSpaces)
            {
                mappedSpaces.Add(spacesGroupedById.MapDtosToResponse());
            }

            return mappedSpaces.ToList();
        }

        public static List<SpaceResponse> MapDtoGroupedByVenueIdToResponses(this List<SpaceDto> spaceDtos)
        {
            List<List<SpaceDto>> groupedSpaces = spaceDtos.GroupSpacesByVenueId();

            List<SpaceResponse> mappedSpaces = new List<SpaceResponse>();

            foreach (List<SpaceDto> spaceGrouping in groupedSpaces)
            {
                List<List<SpaceDto>> groupedSpacesBySpaceId = spaceDtos.ToList().GroupSpacesBySpaceId();

                foreach (List<SpaceDto> spacesGroupedBySpaceId in groupedSpacesBySpaceId)
                {
                    mappedSpaces.Add(spacesGroupedBySpaceId.MapDtosToResponse());
                }
            }

            return mappedSpaces;
        }

        public static SpaceResponse MapDtosToResponse(this IEnumerable<SpaceDto> spaceDtos)
        {
            SpaceDto spaceDto = spaceDtos.FirstOrDefault();

            SpaceResponse model = new SpaceResponse
            {
                SpaceId = spaceDto.SpaceId,
                MaxCapacity = spaceDto.MaxCapacity,
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

        public static VenueResponse MapDtoToResponse(this IEnumerable<VenueDto> venueDtos, List<SpaceResponse> spacesToAddToVenue)
        {
            VenueDto venueDto = venueDtos.FirstOrDefault();

            VenueResponse model = new VenueResponse
            {
                VenueId = venueDto.VenueId,
                Description = venueDto.Description,
                MUrl = venueDto.MUrl,
                Summary = venueDto.Summary,
                Title = venueDto.Title,
                Testimonial = venueDto.Testimonial,
                TestimonialContactEmail = venueDto.TestimonialContactEmail,
                TestimonialContactName = venueDto.TestimonialContactName,
                TestimonialContactOrganisation = venueDto.TestimonialContactOrganisation,
                Spaces = spacesToAddToVenue,
                VenueImages = new List<VenueImageDto>(),
                VenueType = new VenueTypeDto
                {
                    VenueTypeId = venueDto.VenueTypeId,
                    Description = venueDto.VenueTypeDescription
                }                
            };

            foreach (VenueDto venue in venueDtos)
            {
                if (venue.VenueImageId != Guid.Empty && !model.VenueImages.Any(x => x.VenueImageId == venue.VenueImageId))
                    model.VenueImages.Add(new VenueImageDto
                    {
                        Base64VenueImageString = venue.Base64VenueImageString,
                        VenueId = venue.VenueId,
                        VenueImageId = venue.VenueImageId
                    });
            }

            return model;
        }
    }
}