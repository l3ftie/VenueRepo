using Dapper.Contrib.Extensions;
using System;

namespace VLibraries.APIModels
{
    [Table("VenueImage")]
    public class VenueImageDto : VenueImage
    {
        [Key]
        public Guid VenueImageId { get; set; }

        public Guid VenueId { get; set; }
    }
}
