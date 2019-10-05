using System;
using System.Text;

namespace VLibraries.APIModels
{
    public class VenueRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public string Testimonial { get; set; }
        public string TestimonialContactName { get; set; }
        public string TestimonialContactOrganisation { get; set; }
        public string TestimonialContactEmail { get; set; }
        public string MUrl { get; set; }
        public Guid VenueTypeId { get; set; }
        public string Postcode { get; set; }
        public string BuildingNameOrNumber { get; set; }
    }
}
