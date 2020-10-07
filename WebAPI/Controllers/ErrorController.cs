using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAPI.Helpers;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Controller for global error handler
    /// </summary>
    [ApiController]
    public class ErrorController : ControllerBase
    {
        private readonly ILogger<ErrorController> _logger;
        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Global error handler
        /// </summary>
        /// <returns>
        /// In case of server error - HTTP Response 500; In case of logic error - HTTP Response 400
        /// </returns>
        [Route("/error")]
        public ActionResult<string> Error()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerFeature>().Error;
            //_logger.LogError(exception.Message);

            // if logic error
            if (exception is StudentbyException)
            {
                return StatusCode(400, exception.Message);
            }
            // if server error
            else
            {
                
                return StatusCode(500, "Objevila se chyba");
            }

            
        }
    }
}
