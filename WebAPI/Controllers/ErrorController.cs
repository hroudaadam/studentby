using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;

namespace WebAPI.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        [Route("error")]       
        public string Error()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerFeature>().Error;
            int code;
            string message;

            if (exception is AppLogicException)
            {
                code = 400;
                message = exception.Message;
            }
            else
            {
                code = 500;
                message = "Objevila se chyba na serveru";
            }

            Response.StatusCode = code;
            return message;
        }
    }
}
