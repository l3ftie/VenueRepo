using System.Threading.Tasks;
using VLibraries.Enumerations;

namespace VLibraries.HttpClientWrapper
{
    public interface IHttpClientWrapper
    {
        Task<string> GetHtmlViaHttpRequest(HttpRequestType methodType, string encodedUrl, string json = "");
        Task<byte[]> GetBytesViaHttpRequest(HttpRequestType methodType, string encodedUrl, string json = "");
    }
}
