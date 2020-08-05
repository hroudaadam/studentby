using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestAPI.Helpers;
using TestAPI.Models;

namespace TestAPI.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        public ActionResult<ErrorResponse> Error()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerFeature>().Error;

            if (exception is StudentbyException)
            {
                return StatusCode(400, exception.Message);
            }

            return StatusCode(500, "Objevila se chyba");
        }
    }
}
