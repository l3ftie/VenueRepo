using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace VLibraries.APIModels
{
    public class SpaceRequest
    {
        public int MaxCapacity { get; set; }
        public Guid SpaceTypeId { get; set; }
    }
}
