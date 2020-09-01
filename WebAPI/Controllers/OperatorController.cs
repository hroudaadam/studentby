using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Helpers;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = Role.Operator)]
    [ApiController]
    public class OperatorController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IJobApplicationService _jobApplicationService;
        private readonly IGroupService _groupService;

        public OperatorController(ICustomerService customerService, IJobApplicationService jobApplicationService, IGroupService groupService)
        {
            _customerService = customerService;
            _jobApplicationService = jobApplicationService;
            _groupService = groupService;
        }

        // POST: api/operator/customer
        [HttpPost("customer")]
        public async Task<ActionResult<CustomerResponse>> CreateCustomer([FromBody] CustomerRequest request)
        {
            var response = await _customerService.CreateCustomerAsync(request);
            return StatusCode(201, response);
        }

        // GET: api/operator/job-applications
        [HttpGet("job-applications")]
        public async Task<ActionResult<IEnumerable<JobApplicationSimpleResponse>>> GetJobApplications()
        {
            var response = await _jobApplicationService.GetPendingApplicationsAsync();
            return StatusCode(200, response);       
        }

        // GET: api/operator/job-applications/:id
        [HttpGet("job-applications/{id}")]
        public async Task<ActionResult<JobApplicationDetailWithStudentResponse>> GetJobApplicationDetail([FromRoute] int id)
        {
            var response = await _jobApplicationService.GetApplicationDetailOperatorAsync(id);
            if (response == null)
            {
                return StatusCode(404);
            }
            return StatusCode(200, response);
        }

        // PUT: api/operator/job-applications/:id
        [HttpPut("job-applications/{id}")]
        public async Task<ActionResult<JobApplicationDetailResponse>> EditJobApplication(
            [FromRoute] int id, 
            [FromBody] JobApplicationStateRequest request)
        {
            var response = await _jobApplicationService.EditJobApplicationStateAsync(id, request);
            if (response == null)
            {
                return StatusCode(404);
            }
            return StatusCode(204);
        }

        // GET: api/operator/groups
        [HttpGet("groups")]
        public async Task<ActionResult<IEnumerable<GroupResponse>>> GetGroups()
        {
            var response = await _groupService.GetAllAsync();
            return StatusCode(200, response);
        }

        // POST: api/operator/groups
        [HttpPost("groups")]
        public async Task<ActionResult<GroupResponse>> CreateGroup([FromBody] GroupRequest request)
        {
            var response = await _groupService.CreateAsync(request);
            return StatusCode(201, response);
        }
    }
}
