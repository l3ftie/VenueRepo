using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VLibraries.ResponseModels;

namespace VLibraries.BookingService
{
    public interface IBookingService
    {
        Task<ActionResult<ResponseBase<bool>>> AddBookingAsync();
    }
}
