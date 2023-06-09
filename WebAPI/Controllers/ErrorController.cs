﻿using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Controller for exceptions
    /// </summary>
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        [Route("error")]       
        public ErrorRes Error()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerFeature>().Error;

            // BL exception
            if (exception is AppLogicException)
            {
                Response.StatusCode = 400;
                string message = exception.Message;
                string detail = ((AppLogicException)exception).Detail;
                return new ErrorRes(message, detail);
            }
            // server exception
            else
            {
                Response.StatusCode = 500;
                _logger.LogError(exception.Message);
                string message = "Objevila se chyba na serveru";
                return new ErrorRes(message);
            }            
        }
    }
}
