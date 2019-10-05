using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace VLibraries.APIModels
{
    public class SpaceRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public string MUrl { get; set; }
        public int MaxCapacity { get; set; }
        public Guid SpaceTypeId { get; set; }
    }
}
