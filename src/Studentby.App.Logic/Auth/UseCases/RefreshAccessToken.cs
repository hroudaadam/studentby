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

namespace Studentby.App.Logic.Auth.UseCases;

public class RefreshAccessTokenRequest : IRequest<AuthRes>
{
    public string RefreshToken { get; set; }
}

public class RefreshAccessTokenRequestValidator : AbstractValidator<RefreshAccessTokenRequest>
{
    public RefreshAccessTokenRequestValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty();
    }
}

internal class RefreshAccessTokenRequestHandler : IRequestHandler<RefreshAccessTokenRequest, AuthRes>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IClockProvider _clockProvider;
    private readonly ApplicationSettings _applicationSettings;
    private readonly IWebTokenService _webTokenService;
    private readonly ICryptographyService _cryptographyService;

    public RefreshAccessTokenRequestHandler(
        IRefreshTokenRepository refreshTokenRepository,
        IClockProvider clockProvider,
        IOptions<ApplicationSettings> applicationSettings,
        IWebTokenService webTokenService,
        ICryptographyService cryptographyService)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _clockProvider = clockProvider;
        _applicationSettings = applicationSettings.Value;
        _webTokenService = webTokenService;
        _cryptographyService = cryptographyService;
    }

    public async Task<AuthRes> Handle(RefreshAccessTokenRequest request, CancellationToken cancellationToken)
    {
        var refreshToken = await _refreshTokenRepository.GetByTokenAsync(request.RefreshToken);

        if (refreshToken == null ||
            refreshToken.Expiration <= _clockProvider.Now ||
            refreshToken.Revoked) 
        {
            throw new InvalidRefreshTokenException();
        }

        return await AuthCommonLogic.IssueAccessAsync(
            refreshToken.User,
            _applicationSettings,
            _refreshTokenRepository,
            _webTokenService,
            _cryptographyService,
            _clockProvider);
    }
}