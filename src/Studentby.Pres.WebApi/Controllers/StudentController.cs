using Microsoft.AspNetCore.Mvc;
using Studentby.App.Logic.Students.Models;
using Studentby.App.Logic.Students.UseCases;
using Studentby.Pres.WebApi.Atributes;

namespace Studentby.Pres.WebApi.Controllers;

[ApiRoute("students")]
public class StudentController : BaseApiController
{
    [ProducesResponseType(201)]
    [HttpPost]
    public async Task<ActionResult<StudentRes>> Create(CreateStudentRequest body)
    {
        return Created(await Mediate(body));
    }

    [ProducesResponseType(200)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<StudentRes>>> List()
    {
        return Ok(await Mediate(new ListStudentsRequest()));
    }

    [ProducesResponseType(200)]
    [HttpGet("me")]
    public async Task<ActionResult<StudentRes>> GetCurrent()
    {
        return Ok(await Mediate(new GetCurrentStudentDetailRequest()));
    }

    [ProducesResponseType(200)]
    [ProducesErrorResponse(404)]
    [HttpGet("{studentId}")]
    public async Task<ActionResult<StudentRes>> Get(Guid studentId)
    {
        return Ok(await Mediate(new GetStudentDetailRequest(studentId)));
    }

    [ProducesResponseType(200)]
    [ProducesErrorResponse(404)]
    [HttpPut("{studentId}")]
    public async Task<ActionResult<StudentRes>> Update(Guid studentId, EditStudentRequest body)
    {
        ValidateEquals(studentId, body.Id);
        return Ok(await Mediate(body));
    }
}
