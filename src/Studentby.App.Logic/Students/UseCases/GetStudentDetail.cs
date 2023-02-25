using MediatR;
using Studentby.App.Data.Database.Entities.Fields;
using Studentby.App.Data.Database.Repositories;
using Studentby.App.Logic.Common.Attributes;
using Studentby.App.Logic.Students.Exceptions;
using Studentby.App.Logic.Students.Models;

namespace Studentby.App.Logic.Students.UseCases;

[Authorize(Roles = new[] { UserRole.Operator })]
public class GetStudentDetailRequest : IRequest<StudentRes>
{
    public Guid Id { get; set; }

    public GetStudentDetailRequest(Guid id)
    {
        Id = id;
    }
}

internal class GetStudentDetailRequestHandler : IRequestHandler<GetStudentDetailRequest, StudentRes>
{
    private readonly IStudentRepository _studentRepository;

    public GetStudentDetailRequestHandler(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<StudentRes> Handle(GetStudentDetailRequest request, CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetByIdAsync(request.Id);
        if (student == null) throw new StudentNotFoundException();

        return student.Adapt<StudentRes>(s =>
        {
            s.Email = student.User.Email;
        });
    }
}
