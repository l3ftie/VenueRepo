using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using VLibraries.CustomExceptions;
using VLibraries.Errors;
using VLibraries.ExceptionHadler;
using VLibraries.Extensions;
using VLibraries.ResponseModels;

namespace VLibraries.ExceptionHandler
{
    public class ExceptionHandler : IExceptionHandler
    {
        public async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

            ResponseBase<string> response = new ResponseBase<string>
            {
                Error = new Error
                {
                    Target = exception.StackTrace,
                    Message = exception.Message
                }
            };

            await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }

        public async Task HandleHttpStatusCodeResponseExceptionAsync(HttpContext context, HttpStatusCodeResponseException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) exception.HttpStatusCode;

            ResponseBase<string> response = new ResponseBase<string>
            {
                Error = new Error
                {
                    Message = exception.CustomMessage == null 
                    ? exception.HttpStatusCode.GetResponseMessageFromHttpStatusCode() 
                    : exception.CustomMessage,
                    Target = $"{exception.TargetSite.DeclaringType}",
                    InnerError = exception.InnerError
                }
            };

            await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}
