using Microsoft.AspNetCore.Mvc;
using Studentby.App.Logic.JobApplications.Models;
using Studentby.App.Logic.JobApplications.UseCases;
using Studentby.Pres.WebApi.Atributes;

namespace Studentby.Pres.WebApi.Controllers;

[ApiRoute("job-applications")]
public class JobApplicationController : BaseApiController
{
    [ProducesResponseType(201)]
    [HttpPost]
    public async Task<ActionResult<JobApplicationRes>> Create(CreateJobApplicationRequest body)
    {
        return Created(await Mediate(body));
    }

    [ProducesResponseType(200)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<JobApplicationDetailRes>>> List([FromQuery] ListJobApplicationsRequest query)
    {
        return Ok(await Mediate(query));
    }

    [ProducesResponseType(200)]
    [ProducesErrorResponse(404)]
    [HttpGet("{jobApplicationId}")]
    public async Task<ActionResult<JobApplicationDetailRes>> Get(Guid jobApplicationId)
    {
        return Ok(await Mediate(new GetJobApplicationDetailRequest(jobApplicationId)));
    }

    [ProducesResponseType(200)]
    [ProducesErrorResponse(404)]
    [HttpPut("{jobApplicationId}")]
    public async Task<ActionResult<JobApplicationRes>> Update(
        Guid jobApplicationId, 
        EditJobApplicationRquest body)
    {
        ValidateEquals(jobApplicationId, body.Id);
        return Ok(await Mediate(body));
    }
}
