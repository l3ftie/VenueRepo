using System;
using System.Collections.Generic;

namespace VLibraries.APIModels
{
    public class SpaceResponse
    {
        public Guid SpaceId { get; set; }
        public Guid VenueId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public string MUrl { get; set; }
        public int MaxCapacity { get; set; }
        public List<SpaceImageDto> SpaceImages { get; set; }
        public SpaceTypeDto SpaceType { get; set; }
    }
}
