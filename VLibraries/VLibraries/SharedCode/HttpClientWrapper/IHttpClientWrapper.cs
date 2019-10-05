using System.Net.Http;
using System.Threading.Tasks;
using VLibraries.Enumerations;

namespace VLibraries.HttpClientWrapper
{
    public interface IHttpClientWrapper
    {
        Task<string> GetAsync(string url);
        Task<string> PostAsync(string url, string json);
    }
}
