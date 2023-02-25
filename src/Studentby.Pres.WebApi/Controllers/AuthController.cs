using Microsoft.AspNetCore.Mvc;
using Studentby.App.Logic.Auth.Models;
using Studentby.App.Logic.Auth.UseCases;
using Studentby.Pres.WebApi.Atributes;

namespace Studentby.Pres.WebApi.Controllers;

[ApiRoute("auth")]
public class AuthController : BaseApiController
{
    [ProducesResponseType(200)]
    [HttpPost("login")]
    public async Task<ActionResult<AuthRes>> BasicLogin(UserBasicLoginRequest body)
    {
        return Ok(await Mediate(body));
    }

    [ProducesResponseType(200)]
    [HttpPost("refresh-access-token")]
    public async Task<ActionResult<AuthRes>> RefreshAccessToken(RefreshAccessTokenRequest body)
    {
        return Ok(await Mediate(body));
    }

    [ProducesResponseType(204)]
    [HttpPost("revoke-refresh-token")]
    public async Task<ActionResult> RevokeRefreshToken(RevokeRefreshTokenRequest body)
    {
        return NoContent(await Mediate(body));
    }
}
