using Microsoft.AspNetCore.Builder;
using System.Collections.Generic;
using System.Text;

namespace VLibraries.CommonMiddleware
{

    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureCustomExceptionMiddleware<T>(this IApplicationBuilder app) 
            where T : class
        {
            app.UseMiddleware<T>();
        }        
    }
}
