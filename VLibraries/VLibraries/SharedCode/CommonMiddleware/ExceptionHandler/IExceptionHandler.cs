using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using VLibraries.CustomExceptions;

namespace VLibraries.ExceptionHadler
{
    public interface IExceptionHandler
    {
        Task HandleHttpStatusCodeResponseExceptionAsync(HttpContext context, HttpStatusCodeResponseException exception);
        Task HandleExceptionAsync(HttpContext context, Exception exception);
    }
}
