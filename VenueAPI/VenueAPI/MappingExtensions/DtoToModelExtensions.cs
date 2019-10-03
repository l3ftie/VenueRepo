using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VLibraries.APIModels;

namespace VenueAPI.MappingExtensions
{
    public static class DtoToModelExtensions
    {
        public static SpaceResponse MapDtoToResponse(this IEnumerable<SpaceDto> spaceDtos)
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

        public static VenueResponse MapDtoToResponse(this IEnumerable<VenueDto> venueDtos)
        {
            VenueDto venueDto = venueDtos.FirstOrDefault();

            VenueResponse model = new VenueResponse
            {
                Description = venueDto.Description,
                MUrl = venueDto.MUrl,
                Summary = venueDto.Summary,
                Title = venueDto.Title,
                Testimonial = venueDto.Testimonial,
                TestimonialContactEmail = venueDto.TestimonialContactEmail,
                TestimonialContactName = venueDto.TestimonialContactName,
                TestimonialContactOrganisation = venueDto.TestimonialContactOrganisation,
                VenueImages = new List<VenueImageDto>(),
                VenueType = new VenueTypeDto
                {
                    VenueTypeId = venueDto.VenueTypeId,
                    Description = venueDto.VenueTypeDescription
                }                
            };

            foreach (VenueDto venue in venueDtos)
            {
                if (venue.VenueImageId != Guid.Empty)
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
