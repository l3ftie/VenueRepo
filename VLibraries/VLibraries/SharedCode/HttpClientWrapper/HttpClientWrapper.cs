using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VLibraries.CustomExceptions;
using VLibraries.Enumerations;
using VLibraries.Errors;

namespace VLibraries.HttpClientWrapper
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly HttpClient _client;

        public HttpClientWrapper()
        {
            _client = new HttpClient();
        }

        public async Task<string> GetAsync(string url)
        {
            HttpResponseMessage response = await _client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);

            if (!response.IsSuccessStatusCode)
                throw new HttpStatusCodeResponseException(response.StatusCode, new InnerError
                {
                    Message = $"There was a problem making a GET request to: {url}"
                });

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> PostAsync(string url, string json)
        {
            HttpResponseMessage response = await _client.PostAsync(url, new StringContent(json, Encoding.UTF8, HttpResponseContentType.JSON));

            if (!response.IsSuccessStatusCode)
                throw new HttpStatusCodeResponseException(response.StatusCode, new InnerError
                {
                    Message = $"There was a problem making a POST request to: {url} with the following data: {json}"
                });

            return await response.Content.ReadAsStringAsync();
        }
    }
}
