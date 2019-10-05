using System.ComponentModel.DataAnnotations;

namespace VLibraries.APIModels
{
    public class VenueAddress
    {
        public string Postcode { get; set; }
        public string BuildingNameOrNumber { get; set; }

        public string Road { get; set; }
        public string Town { get; set; }
        public string County { get; set; }
        public string DisplayName { get; set; }
        public string Country { get; set; }
        public string Village { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; } //England, Scotlad, etc.
    }    
}
