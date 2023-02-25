using FluentValidation;
using MediatR;
using Studentby.App.Data.Database.Entities;
using Studentby.App.Data.Database.Entities.Fields;
using Studentby.App.Data.Database.Repositories;
using Studentby.App.Logic.Common.Attributes;
using Studentby.App.Logic.Employers.Models;
using Studentby.App.Logic.Groups.Exceptions;
using Studentby.App.Logic.Users;
using Studentby.Shared.Helpers;
using Studentby.Shared.Security;

namespace Studentby.App.Logic.Employers.UseCases;

[Authorize(Roles = new[] { UserRole.Operator })]
public class CreateEmployerRequest : IRequest<CreatedEmployerRes>
{
    public Guid GroupId { get; set; }
    public string Email { get; set; }
    public string JobTitle { get; set; }
}

public class CreateEmployerRequestValidator : AbstractValidator<CreateEmployerRequest>
{
    public CreateEmployerRequestValidator()
    {
        RuleFor(x => x.Email)
            .MaximumLength(255)
            .EmailAddress()
            .NotEmpty();
        RuleFor(x => x.JobTitle)
            .MaximumLength(50)
            .NotEmpty();
    }
}

internal class CreateEmployerRequestHandler : IRequestHandler<CreateEmployerRequest, CreatedEmployerRes>
{
    private readonly IEmployerRepository _employerRepository;
    private readonly IGroupRepository _groupRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICryptographyService _cryptographyService;

    public CreateEmployerRequestHandler(
        IEmployerRepository employerRepository,
        IGroupRepository groupRepository,
        IUserRepository userRepository,
        ICryptographyService cryptographyService
        )
    {
        _employerRepository = employerRepository;
        _groupRepository = groupRepository;
        _userRepository = userRepository;
        _cryptographyService = cryptographyService;
    }

    public async Task<CreatedEmployerRes> Handle(CreateEmployerRequest request, CancellationToken cancellationToken)
    {
        var group = await _groupRepository.GetByIdAsync(request.GroupId);
        if (group == null) throw new GroupNotFoundException();

        string password = _cryptographyService.GetRandomString(12);
        var user = await UserCommonLogic
            .CreateUserEntityAsync(request.Email, password, UserRole.Employer, _userRepository, _cryptographyService);
        Guard.NotNull(user);

        string changePasswordSecret = UserCommonLogic.CreateChangePasswordSecret(user);

        Employer employer = request.Adapt<Employer>(s =>
        {
            s.User = user;
        });

        employer = await _employerRepository.CreateAsync(employer);
        return employer.Adapt<CreatedEmployerRes>(e =>
        {
            e.Email = employer.User.Email;
            e.ChangePasswordSecret = changePasswordSecret;
        });
    }
}
