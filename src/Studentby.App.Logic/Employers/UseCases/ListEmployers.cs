using MediatR;
using Studentby.App.Data.Database.Entities.Fields;
using Studentby.App.Data.Database.Repositories;
using Studentby.App.Logic.Common.Attributes;
using Studentby.App.Logic.Employers.Models;
using System.Linq;

namespace Studentby.App.Logic.Employers.UseCases;

[Authorize(Roles = new[] { UserRole.Operator })]
public class ListEmployersRequest : IRequest<IEnumerable<EmployerRes>>
{
    public Guid? GroupId { get; set; }
}

internal class ListEmployersRequestHandler : IRequestHandler<ListEmployersRequest, IEnumerable<EmployerRes>>
{
    private readonly IEmployerRepository _employerRepository;

    public ListEmployersRequestHandler(IEmployerRepository employerRepository)
    {
        _employerRepository = employerRepository;
    }

    public async Task<IEnumerable<EmployerRes>> Handle(ListEmployersRequest request, CancellationToken cancellationToken)
    {
        var employers = await _employerRepository.FiltredListAsync(request.GroupId);
        return employers.Select(e => e.Adapt<EmployerRes>(er =>
        {
            er.Email = e.User.Email;
        }));
    }
}
