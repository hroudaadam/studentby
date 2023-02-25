using MediatR;
using Studentby.App.Data.Database.Entities.Fields;
using Studentby.App.Data.Database.Repositories;
using Studentby.App.Logic.Common.Attributes;
using Studentby.App.Logic.Students.Models;
using System.Linq;

namespace Studentby.App.Logic.Students.UseCases;

[Authorize(Roles = new[] { UserRole.Operator })]
public class ListStudentsRequest : IRequest<IEnumerable<StudentRes>>
{
}

internal class ListStudentsRequestHandler : IRequestHandler<ListStudentsRequest, IEnumerable<StudentRes>>
{
    private readonly IStudentRepository _studentRepository;

    public ListStudentsRequestHandler(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<IEnumerable<StudentRes>> Handle(ListStudentsRequest request, CancellationToken cancellationToken)
    {
        var students = await _studentRepository.ListAsync();
        return students.Select(st => st.Adapt<StudentRes>(sr =>
        {
            sr.Email = st.User.Email;
        }));
    }
}
