using FluentValidation;
using MediatR;
using Studentby.App.Data.Database.Entities;
using Studentby.App.Data.Database.Repositories;
using Studentby.App.Logic.Auth.Exceptions;
using Studentby.App.Logic.Auth.Models;
using Studentby.App.Logic.Common;
using Studentby.App.Logic.Common.Attributes;
using Studentby.App.Logic.Common.Security;
using Studentby.Shared.Clock;
using Studentby.Shared.Security;

namespace Studentby.App.Logic.Auth.UseCases;

[Authorize]
public class RevokeRefreshTokenRequest : IRequest<bool>
{
    public string RefreshToken { get; set; }
}

public class RevokeRefreshTokenRequestValidator : AbstractValidator<RevokeRefreshTokenRequest>
{
    public RevokeRefreshTokenRequestValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty();
    }
}

internal class RevokeRefreshTokenRequestHandler : IRequestHandler<RevokeRefreshTokenRequest, bool>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IClockProvider _clockProvider;
    private readonly ISecurityContextProvider _securityContextProvider;

    public RevokeRefreshTokenRequestHandler(
        IRefreshTokenRepository refreshTokenRepository,
        IClockProvider clockProvider,
        ISecurityContextProvider securityContextProvider)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _clockProvider = clockProvider;
        _securityContextProvider = securityContextProvider;
    }

    public async Task<bool> Handle(RevokeRefreshTokenRequest request, CancellationToken cancellationToken)
    {
        Guid userId = _securityContextProvider.GetSubjectId();
        var refreshToken = await _refreshTokenRepository.GetByTokenAndUserIdAsync(request.RefreshToken, userId);

        if (refreshToken == null ||
            refreshToken.Expiration <= _clockProvider.Now ||
            refreshToken.Revoked) 
        {
            throw new InvalidRefreshTokenException();
        }

        refreshToken.Revoked = true;
        await _refreshTokenRepository.UpdateAsync(refreshToken);

        return true;
    }
}