using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VLibraries.APIModels;

namespace VenueAPI.MappingExtensions
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
    }
}
