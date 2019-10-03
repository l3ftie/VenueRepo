using Dapper.Contrib.Extensions;
using System;

namespace VLibraries.APIModels
{
    [Table("Space")]
    public class SpaceDto : SpaceRequest
    {
        [Key]
        public Guid SpaceId { get; set; }

        public Guid VenueId { get; set; }
        public Guid SpaceImageId { get; set; }
        public string SpaceTypeDescription{ get; set; }
        public string Base64SpaceImageString { get; set; }
    }
}
