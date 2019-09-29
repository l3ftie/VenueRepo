using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace VLibraries.APIModels
{
    [Table("Space")]
    public class SpaceDto : Space
    {
        [Key]
        public Guid SpaceId { get; set; }

        [ExplicitKey]
        public Guid VenueId { get; set; }

        [Write(false)]
        public List<SpaceImageDto> SpaceImages { get; set; }

        [Write(false)]
        public SpaceTypeDto SpaceType { get; set; }
    }
}
