using Dapper.Contrib.Extensions;
using System;

namespace VLibraries.APIModels
{
    [Table("VenueImage")]
    public class VenueImageDto : VenueImage
    {
        [Key]
        public Guid VenueImageId { get; set; }

        [ExplicitKey]
        public Guid VenueId { get; set; }
    }
}
