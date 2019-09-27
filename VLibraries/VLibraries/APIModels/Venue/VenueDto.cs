using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace VLibraries.APIModels
{
    [Table("Venue")]
    public class VenueDto : VenueRequest
    {
        [ExplicitKey]
        public Guid VenueId { get; set; }

        [Write(false)]
        public List<SpaceRequest> Spaces { get; set; }
    }
}
