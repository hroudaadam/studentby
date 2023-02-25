using Microsoft.AspNetCore.Mvc;
using Studentby.Pres.WebApi.Atributes;
using Studentby.Pres.WebApi.Exceptions;

namespace Studentby.Pres.WebApi.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public class FallbackController : BaseApiController
{
    [ApiRoute("{**rest}")]
    public IActionResult Fallback() => throw new PathNotFoundException();
}
