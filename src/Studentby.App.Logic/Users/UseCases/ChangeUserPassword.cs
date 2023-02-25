using FluentValidation;
using MediatR;
using Studentby.App.Data.Database.Entities;
using Studentby.App.Data.Database.Repositories;
using Studentby.App.Logic.Students.UseCases;
using Studentby.App.Logic.Users.Exceptions;
using Studentby.Shared.Clock;
using Studentby.Shared.Security;
using System.Reflection;

namespace Studentby.App.Logic.Users.UseCases;

public class ChangeUserPasswordRequest : IRequest<bool>
{
    public string ChangePasswordSecret { get; set; }
    public string Password { get; set; }
}

public class ChangeUserPasswordRequestValidator : AbstractValidator<ChangeUserPasswordRequest>
{
    public ChangeUserPasswordRequestValidator()
    {
        RuleFor(x => x.ChangePasswordSecret)
            .Must(cps => cps.Split(':').Length == 2)
            .WithMessage("{PropertyName} is not valid")
            .NotEmpty();
        RuleFor(x => x.Password)
            .MinimumLength(8)
            .NotEmpty();
    }
}

internal class ChangeUserPasswordRequestHandler : IRequestHandler<ChangeUserPasswordRequest, bool>
{
    private readonly IUserRepository _userRepository;
    private readonly ICryptographyService _cryptographyService;

    public ChangeUserPasswordRequestHandler(IUserRepository userRepository, ICryptographyService cryptographyService)
    {
        _userRepository = userRepository;
        _cryptographyService = cryptographyService;
    }

    public async Task<bool> Handle(ChangeUserPasswordRequest request, CancellationToken cancellationToken)
    {
        string email = request.ChangePasswordSecret.Split(':')[0];

        User user = await _userRepository.GetByEmailAsync(email);
        if (user == null || UserCommonLogic.CreateChangePasswordSecret(user) != request.ChangePasswordSecret)
        {
            throw new InvalidChangePasswordSecretException();
        }

        (byte[] hash, byte[] salt) = _cryptographyService.GetHashAndSalt(request.Password);
        user.PasswordHash = hash;
        user.PasswordSalt = salt;

        user = await _userRepository.UpdateAsync(user);

        return true;
    }
}
