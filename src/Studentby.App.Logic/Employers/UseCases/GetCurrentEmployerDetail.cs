using MediatR;
using Studentby.App.Data.Database.Entities.Fields;
using Studentby.App.Data.Database.Repositories;
using Studentby.App.Logic.Common.Attributes;
using Studentby.App.Logic.Common.Security;
using Studentby.App.Logic.Employers.Models;
using Studentby.Shared.Helpers;

namespace Studentby.App.Logic.Employers.UseCases;

[Authorize(Roles = new[] { UserRole.Employer })]
public class GetCurrentEmployerDetailRequest : IRequest<EmployerRes>
{
}

internal class GetCurrentEmployerDetailRequestHandler : IRequestHandler<GetCurrentEmployerDetailRequest, EmployerRes>
{
    private readonly IEmployerRepository _employerRepository;
    private readonly ISecurityContextProvider _securityContextProvider;

    public GetCurrentEmployerDetailRequestHandler(IEmployerRepository employerRepository, ISecurityContextProvider securityContextProvider)
    {
        _employerRepository = employerRepository;
        _securityContextProvider = securityContextProvider;
    }

    public async Task<EmployerRes> Handle(GetCurrentEmployerDetailRequest request, CancellationToken cancellationToken)
    {
        Guid userId = _securityContextProvider.GetSubjectId();
        var employer = await _employerRepository.GetByUserIdAsync(userId);
        Guard.NotNull(employer);

        return employer.Adapt<EmployerRes>(e =>
        {
            e.Email = employer.User.Email;
        });
    }
}
