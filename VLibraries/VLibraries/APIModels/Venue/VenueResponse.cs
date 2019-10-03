using System.Collections.Generic;

namespace VLibraries.APIModels
{
    public class VenueResponse
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public string Testimonial { get; set; }
        public string TestimonialContactName { get; set; }
        public string TestimonialContactOrganisation { get; set; }
        public string TestimonialContactEmail { get; set; }
        public string MUrl { get; set; }
        public List<SpaceResponse> Spaces { get; set; }
        public List<VenueImageDto> VenueImages { get; set; }
        public VenueTypeDto VenueType { get; set; }
    }
}
