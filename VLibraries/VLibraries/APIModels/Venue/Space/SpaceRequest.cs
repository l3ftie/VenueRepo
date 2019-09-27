using System;

namespace VLibraries.APIModels
{
    public class SpaceDto : SpaceRequest
    {
        public Guid SpaceId { get; set; }
    }

    public class SpaceRequest
    {
        public int MaxCapacity { get; set; }
        public SpaceType SpaceType { get; set; }
    }
}
