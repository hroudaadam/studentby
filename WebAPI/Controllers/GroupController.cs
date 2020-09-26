using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;
using WebAPI.Helpers;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/groups")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        // GET: api/groups
        [HttpGet]
        [Authorize(Roles = UserRoles.Operator)]
        public async Task<ActionResult<IEnumerable<GroupRes>>> GetAll()
        {
            var response = await _groupService.GetListAsync();
            return StatusCode(200, response);
        }

        // GET: api/groups/1
        [HttpGet("{groupId}")]
        [Authorize(Roles = UserRoles.Operator)]
        public async Task<ActionResult<GroupWithCustsRes>> Get([FromRoute] int groupId)
        {
            var response = await _groupService.GetDetailAsync(groupId);
            if (response == null)
            {
                return StatusCode(404);
            }
            return StatusCode(200, response);
        }

        // POST: api/groups
        [HttpPost]
        [Authorize(Roles = UserRoles.Operator)]
        public async Task<ActionResult<GroupRes>> Post([FromBody] GroupReq request)
        {
            var response = await _groupService.CreateAsync(request);
            return StatusCode(201, response);
        }

        // PUT: api/groups/1
        [HttpPost("{groupId}")]
        [Authorize(Roles = UserRoles.Operator)]
        public async Task<ActionResult<GroupRes>> Put([FromBody] GroupWithIdReq request, [FromRoute] int groupId)
        {
            var successful = await _groupService.EditAsync(request, groupId);
            if (successful)
            {
                return StatusCode(204);
            }
            return StatusCode(404);
        }
    }
}
