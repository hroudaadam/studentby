using Microsoft.AspNetCore.Mvc;
using Studentby.App.Logic.Students.Models;
using Studentby.App.Logic.Students.UseCases;
using Studentby.App.Logic.Users.UseCases;
using Studentby.Pres.WebApi.Atributes;

namespace Studentby.Pres.WebApi.Controllers;

[ApiRoute("users")]
public class UserController : BaseApiController
{
    [ProducesResponseType(204)]
    [HttpPut("password")]
    public async Task<ActionResult> UpdatePassword(ChangeUserPasswordRequest body)
    {
        return NoContent(await Mediate(body));
    }
}
