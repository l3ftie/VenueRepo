using System;

namespace VLibraries.APIModels
{
    public class Space
    {
        public Guid SpaceId { get; set; }
        public int MaxCapacity { get; set; }
        public SpaceType SpaceType { get; set; }
    }
}
