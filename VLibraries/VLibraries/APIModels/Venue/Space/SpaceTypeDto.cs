using Dapper.Contrib.Extensions;
using System;

namespace VLibraries.APIModels
{
    [Table("SpaceType")]
    public class SpaceTypeDto : SpaceType
    {
        [Key]
        public Guid SpaceTypeId { get; set; }
    }
}
