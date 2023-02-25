using Microsoft.AspNetCore.Mvc;
using Studentby.App.Logic.Employers.Models;
using Studentby.App.Logic.Employers.UseCases;
using Studentby.Pres.WebApi.Atributes;

namespace Studentby.Pres.WebApi.Controllers;

[ApiRoute("employers")]
public class EmployerController : BaseApiController
{
    [ProducesResponseType(201)]
    [HttpPost]
    public async Task<ActionResult<CreatedEmployerRes>> Create(CreateEmployerRequest body)
    {
        return Created(await Mediate(body));
    }

    [ProducesResponseType(200)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmployerRes>>> List([FromQuery] ListEmployersRequest query)
    {
        return Ok(await Mediate(query));
    }

    [ProducesResponseType(200)]
    [HttpGet("me")]
    public async Task<ActionResult<EmployerRes>> GetCurrent()
    {
        return Ok(await Mediate(new GetCurrentEmployerDetailRequest()));
    }
}
