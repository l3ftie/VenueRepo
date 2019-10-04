using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VLibraries.APIModels;

namespace VenueAPI.Extensions
{
    public static class RequestToDtoExtensions
    {
        public static VenueDto MapRequestToDto(this VenueRequest venue, Guid venueId)
        {
            return new VenueDto
            {
                VenueId = venueId,
                Title = venue.Title,
                Description = venue.Description,
                MUrl = venue.MUrl,
                Summary = venue.Summary,
                Testimonial = venue.Testimonial,
                TestimonialContactEmail = venue.TestimonialContactEmail,
                TestimonialContactName = venue.TestimonialContactName,
                TestimonialContactOrganisation = venue.TestimonialContactOrganisation,
                VenueTypeId = venue.VenueTypeId
            };
        }

        public static SpaceDto MapRequestToDto(this SpaceRequest space, Guid venueId, Guid spaceId)
        {
            return new SpaceDto
            {
                MaxCapacity = space.MaxCapacity,
                SpaceTypeId = space.SpaceTypeId,
                VenueId = venueId,
                SpaceId = spaceId
            };
        }
    }
}
