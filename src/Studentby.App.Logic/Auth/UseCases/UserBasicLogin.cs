using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using Studentby.App.Data.Database.Entities;
using Studentby.App.Data.Database.Repositories;
using Studentby.App.Logic.Auth.Exceptions;
using Studentby.App.Logic.Auth.Models;
using Studentby.App.Logic.Common;
using Studentby.Shared.Clock;
using Studentby.Shared.Security;
using System.Security.Claims;

namespace Studentby.App.Logic.Auth.UseCases;

public class UserBasicLoginRequest : IRequest<AuthRes>
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class UserBasicLoginRequestValidator : AbstractValidator<UserBasicLoginRequest>
{
    public UserBasicLoginRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
        RuleFor(x => x.Password)
            .NotEmpty();
    }
}

internal class UserBasicLoginHandler : IRequestHandler<UserBasicLoginRequest, AuthRes>
{
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly ICryptographyService _cryptographyService;
    private readonly IWebTokenService _webTokenService;
    private readonly IClockProvider _clockProvider;
    private readonly ApplicationSettings _applicationSettings;

    public UserBasicLoginHandler(
        IUserRepository userRepository,
        IRefreshTokenRepository refreshTokenRepository,
        ICryptographyService cryptographyService,
        IWebTokenService webTokenService,
        IClockProvider clockProvider,
        IOptions<ApplicationSettings> applicationSettings)
    {
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _cryptographyService = cryptographyService;
        _webTokenService = webTokenService;
        _clockProvider = clockProvider;
        _applicationSettings = applicationSettings.Value;
    }

    public async Task<AuthRes> Handle(UserBasicLoginRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user == null || !_cryptographyService.VerifyHashAndSalt(request.Password, user.PasswordHash, user.PasswordSalt))
        {
            throw new InvalidEmailPasswordException();
        }

        return await AuthCommonLogic.IssueAccessAsync(
            user,
            _applicationSettings,
            _refreshTokenRepository,
            _webTokenService,
            _cryptographyService,
            _clockProvider);
    }
}