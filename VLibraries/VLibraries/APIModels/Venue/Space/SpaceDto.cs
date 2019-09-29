using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace VLibraries.APIModels
{
    [Table("Space")]
    public class SpaceDto : Space
    {
        [ExplicitKey]
        public Guid SpaceId { get; set; }
        public Guid VenueId { get; set; }

        [Write(false)]
        public new List<SpaceImageDto> SpaceImages { get; set; }
    }
}
