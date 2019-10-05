using Newtonsoft.Json;

namespace VLibraries.APIModels
{
    public class LocationIqAddress
    {
        [JsonProperty("road")]
        public string Road { get; set; }

        [JsonProperty("town")]
        public string Town { get; set; }

        [JsonProperty("state_district")]
        public string County { get; set; }

        [JsonProperty("postcode")]
        public string Postcode { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("suburb")]
        public string Suburb { get; set; }

        [JsonProperty("village")]
        public string Village { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }
    }
}
