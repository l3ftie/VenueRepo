using VLibraries.Errors;

namespace VLibraries.ResponseModels
{
    public class ResponseBase<T>
    {
        public T Response { get; set; }
        public Error Error { get; set; }

        public ResponseBase(T Response, Error Error = null)
        {
            this.Response = Response;
            this.Error = Error;
        }

        public ResponseBase()
        {
        }
    }
}
