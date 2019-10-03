using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace VLibraries.APIModels
{
    [Table("Venue")]
    public class VenueDto : VenueRequest
    {
        [Key]
        public Guid VenueId { get; set; }

        public Guid VenueImageId { get; set; }
        public string VenueTypeDescription { get; set; }
        public string Base64VenueImageString { get; set; }
    }
}
