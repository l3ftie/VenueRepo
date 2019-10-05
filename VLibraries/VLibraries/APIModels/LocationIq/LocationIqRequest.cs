using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VLibraries.APIModels
{
    public class LocationIqForwardRequest
    {
        public string SearchTerm { get; set; }
    }

    public class LocationIqReverseRequest
    {
        public string Latitude { get; set; }

        public string Longitude { get; set; }
    }
}
