using System.Net;

namespace VLibraries.Extensions
{
    public static class HttpStatusCodeExtensions
    {
        public static string GetResponseMessageFromHttpStatusCode(this HttpStatusCode statusCode)
        {
            switch (statusCode)
            {
                case HttpStatusCode.NotFound:
                    return "Nothing Found";

                case HttpStatusCode.BadRequest:
                    return "Bad Request";

                case HttpStatusCode.BadGateway:
                    return "Bad Gateway";

                default:
                    return "The request could not be processed at this time";
            }
        }
    }
}
