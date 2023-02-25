using FluentValidation;
using MediatR;
using Studentby.App.Data.Database.Entities;
using Studentby.App.Data.Database.Entities.Fields;
using Studentby.App.Data.Database.Repositories;
using Studentby.App.Logic.Common.Models;
using Studentby.App.Logic.Students.Models;
using Studentby.App.Logic.Users;
using Studentby.Shared.Clock;
using Studentby.Shared.Helpers;
using Studentby.Shared.Security;

namespace Studentby.App.Logic.Students.UseCases;

public class CreateStudentRequest : IRequest<StudentRes>
{
    public string Email { get; set; }

    public string Password { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime DateOfBirth { get; set; }

    public AddressReq Address { get; set; }
}

public class CreateStudentRequestValidator : AbstractValidator<CreateStudentRequest>
{
    public CreateStudentRequestValidator(IClockProvider clockProvider)
    {
        RuleFor(x => x.Email)
            .MaximumLength(255)
            .EmailAddress()
            .NotEmpty();
        RuleFor(x => x.Password)
            .MinimumLength(8)
            .NotEmpty();
        RuleFor(x => x.FirstName)
            .MaximumLength(50)
            .NotEmpty();
        RuleFor(x => x.LastName)
            .MaximumLength(50)
            .NotEmpty();
        RuleFor(x => x.DateOfBirth)
            .Must(dob => dob.AddYears(18).Date < clockProvider.Now.Date)
            .WithMessage("Age must be over 18 years");
    }
}

internal class CreateStudentRequestHandler : IRequestHandler<CreateStudentRequest, StudentRes>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICryptographyService _cryptographyService;

    public CreateStudentRequestHandler(
        IStudentRepository studentRepository,
        IUserRepository userRepository,
        ICryptographyService cryptographyService)
    {
        _studentRepository = studentRepository;
        _userRepository = userRepository;
        _cryptographyService = cryptographyService;
    }

    public async Task<StudentRes> Handle(CreateStudentRequest request, CancellationToken cancellationToken)
    {
        var user = await UserCommonLogic
            .CreateUserEntityAsync(request.Email, request.Password, UserRole.Student, _userRepository, _cryptographyService);
        Guard.NotNull(user);

        Student student = request.Adapt<Student>(s =>
        {
            s.User = user;
        });

        student = await _studentRepository.CreateAsync(student);
        return student.Adapt<StudentRes>(s =>
        {
            s.Email = student.User.Email;
        });
    }
}
