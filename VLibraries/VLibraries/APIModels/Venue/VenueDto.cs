using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace VLibraries.APIModels
{
    [Table("Venue")]
    public class VenueDto : Venue
    {
        [Key]
        public Guid VenueId { get; set; }

        [Write(false)]
        public List<SpaceDto> Spaces { get; set; }

        [Write(false)]
        public new List<VenueImageDto> VenueImages { get; set; }
    }
}
