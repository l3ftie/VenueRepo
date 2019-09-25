﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using VLibraries.CustomExceptions;
using VLibraries.ExceptionHadler;

namespace BookingAPI
{
    public class BookingAPIExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IExceptionHandler _exceptionHandler;
        private readonly ILogger<BookingAPIExceptionMiddleware> _logger;

        public BookingAPIExceptionMiddleware(RequestDelegate next, IExceptionHandler exceptionHandler, ILogger<BookingAPIExceptionMiddleware> logger)
        {
            _exceptionHandler = exceptionHandler;
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (HttpStatusCodeResponseException ex)
            {
                _logger.LogError($"HTTP Status Code Response: {(int)ex.HttpStatusCode} Exception:  {ex}");

                await _exceptionHandler.HandleHttpStatusCodeResponseExceptionAsync(httpContext, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex}");

                await _exceptionHandler.HandleExceptionAsync(httpContext, ex);
            }
        }
    }
}
