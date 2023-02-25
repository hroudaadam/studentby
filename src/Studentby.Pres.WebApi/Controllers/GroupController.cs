using Microsoft.AspNetCore.Mvc;
using Studentby.App.Logic.Groups.Models;
using Studentby.App.Logic.Groups.UseCases;
using Studentby.Pres.WebApi.Atributes;

namespace Studentby.Pres.WebApi.Controllers;

[ApiRoute("groups")]
public class GroupController : BaseApiController
{
    [ProducesResponseType(201)]
    [HttpPost]
    public async Task<ActionResult<GroupRes>> Create(CreateGroupRequest body)
    {
        return Created(await Mediate(body));
    }

    [ProducesResponseType(200)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GroupRes>>> List()
    {
        return Ok(await Mediate(new ListGroupsRequest()));
    }

    [ProducesResponseType(200)]
    [ProducesErrorResponse(404)]
    [HttpGet("{groupId}")]
    public async Task<ActionResult<GroupRes>> Get(Guid groupId)
    {
        return Ok(await Mediate(new GetGroupDetailRequest(groupId)));
    }
}
