using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VLibraries.APIModels
{
    public class LocationIqForwardResponse
    {
        [JsonProperty("place_id")]
        public string LocationIqPlaceId { get; set; }

        [JsonProperty("lat")]
        public string Latitude { get; set; }

        [JsonProperty("lon")]
        public string Longitude { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }    

    public class LocationIqReverseResponse
    {
        [JsonProperty("place_id")]
        public string PlaceId { get; set; }

        [JsonProperty("lat")]
        public string Latitude { get; set; }

        [JsonProperty("lon")]
        public string Longitude { get; set; }
                
        [JsonProperty("address")]
        public LocationIqAddress Address { get; set; }
    }
}
