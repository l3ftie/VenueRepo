using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using VLibraries.APIAbstractions;
using VLibraries.APIModels;
using VLibraries.HttpClientWrapper;
using VLibraries.SharedCode.Extensions;

namespace VenueAPI.Services
{
    public class LocationIqProxy : ILocationIqProxy
    {
        private readonly IHttpClientWrapper _httpClient;
        private readonly string _locationIqApiKey;

        public LocationIqProxy(IHttpClientWrapper httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _locationIqApiKey = config.GetSection("LocationIqApiKey").Value;
        }

        public async Task<LocationIqReverseResponse> GetLocationDetailsAsync(string postcode)
        {
            string searchTerm = postcode.IsPostcode() ? postcode.SplitPostcode() : postcode;

            string forwardSearchResult = await _httpClient.GetAsync($"https://eu1.locationiq.com/v1/search.php?key={_locationIqApiKey}&q={searchTerm}&countrycodes=gb&format=json");

            List<LocationIqForwardResponse> locationIqForwardResponse = JsonConvert.DeserializeObject<List<LocationIqForwardResponse>>(forwardSearchResult);

            string reverseLookupResult = await _httpClient.GetAsync($"https://eu1.locationiq.com/v1/reverse.php?key={_locationIqApiKey}&lat={locationIqForwardResponse[0].Latitude}&lon={locationIqForwardResponse[0].Longitude}&countrycodes=gb&format=json");

            return JsonConvert.DeserializeObject<LocationIqReverseResponse>(reverseLookupResult);
        }
    }
}