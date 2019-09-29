using System.Collections.Generic;

namespace VLibraries.APIModels
{
    public class Space
    {
        public int MaxCapacity { get; set; }
        public SpaceType SpaceType { get; set; }
        public List<SpaceImage> SpaceImages {get;set;}
    }
}
