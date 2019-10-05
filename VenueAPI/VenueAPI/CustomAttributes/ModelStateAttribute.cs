using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using VLibraries.CustomExceptions;

namespace VenueAPI.CustomAttributes
{
    public class CustomModelStateValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                throw new HttpStatusCodeResponseException(HttpStatusCode.BadRequest, JsonConvert.SerializeObject(context.ModelState.Keys));
            }
        }
    }
}
