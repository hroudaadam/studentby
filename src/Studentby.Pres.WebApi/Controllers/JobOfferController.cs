using Microsoft.AspNetCore.Mvc;
using Studentby.App.Logic.JobOffers.Models;
using Studentby.App.Logic.JobOffers.UseCases;
using Studentby.Pres.WebApi.Atributes;

namespace Studentby.Pres.WebApi.Controllers;

[ApiRoute("job-offers")]
public class JobOfferController : BaseApiController
{
    [ProducesResponseType(201)]
    [HttpPost]
    public async Task<ActionResult<JobOfferRes>> Create(CreateJobOfferRequest body)
    {
        return Created(await Mediate(body));
    }

    [ProducesResponseType(200)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<JobOfferRes>>> List([FromQuery] ListJobOffersRequest query)
    {
        return Ok(await Mediate(query));
    }

    [ProducesResponseType(200)]
    [ProducesErrorResponse(404)]
    [HttpGet("{jobOfferId}")]
    public async Task<ActionResult<JobOfferRes>> Get(Guid jobOfferId)
    {
        return Ok(await Mediate(new GetJobOfferDetailRequest(jobOfferId)));
    }
}
