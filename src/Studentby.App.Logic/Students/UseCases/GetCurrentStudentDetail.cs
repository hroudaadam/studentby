using MediatR;
using Studentby.App.Data.Database.Entities.Fields;
using Studentby.App.Data.Database.Repositories;
using Studentby.App.Logic.Common.Attributes;
using Studentby.App.Logic.Common.Security;
using Studentby.App.Logic.Students.Models;
using Studentby.Shared.Helpers;

namespace Studentby.App.Logic.Students.UseCases;

[Authorize(Roles = new[] { UserRole.Student })]
public class GetCurrentStudentDetailRequest : IRequest<StudentRes>
{
}

internal class GetCurrentStudentDetailRequestHandler : IRequestHandler<GetCurrentStudentDetailRequest, StudentRes>
{
    private readonly IStudentRepository _studentRepository;
    private readonly ISecurityContextProvider _securityContextProvider;

    public GetCurrentStudentDetailRequestHandler(IStudentRepository studentRepository, ISecurityContextProvider securityContextProvider)
    {
        _studentRepository = studentRepository;
        _securityContextProvider = securityContextProvider;
    }

    public async Task<StudentRes> Handle(GetCurrentStudentDetailRequest request, CancellationToken cancellationToken)
    {
        Guid userId = _securityContextProvider.GetSubjectId();
        var student = await _studentRepository.GetByUserIdAsync(userId);
        Guard.NotNull(student);

        return student.Adapt<StudentRes>(s =>
        {
            s.Email = student.User.Email;
        });
    }
}
