﻿using Dapper.Contrib.Extensions;
using System;

namespace VLibraries.APIModels
{
    [Table("SpaceType")]
    public class SpaceTypeDto
    {
        [Key]
        public Guid SpaceTypeId { get; set; }
        public string Description { get; set; }
    }
}
