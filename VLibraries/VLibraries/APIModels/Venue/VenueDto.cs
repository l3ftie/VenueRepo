﻿using Dapper.Contrib.Extensions;
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
