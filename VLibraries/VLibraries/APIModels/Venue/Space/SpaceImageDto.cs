using Dapper.Contrib.Extensions;
using System;

namespace VLibraries.APIModels
{
    [Table("SpaceImage")]
    public class SpaceImageDto : SpaceImage
    {
        [ExplicitKey]
        public Guid SpaceImageId { get; set; }
        public Guid SpaceId { get; set; }
    }
}
