using Studentby.App.Data.Database.Entities;
using Studentby.App.Data.Database.Repositories;
using Studentby.App.Logic.Auth.Models;
using Studentby.App.Logic.Common;
using Studentby.Shared.Clock;
using Studentby.Shared.Security;
using System.Security.Claims;

namespace Studentby.App.Logic.Auth;

internal static class AuthCommonLogic
{
    internal static async Task<AuthRes> IssueAccessAsync(
        User user,
        ApplicationSettings applicationSettings,
        IRefreshTokenRepository refreshTokenRepository,
        IWebTokenService webTokenService,
        ICryptographyService cryptographyService,
        IClockProvider clockProvider
        )
    {
        List<Claim> claims = new()
        {
            new Claim("sub", user.Id.ToString()),
            new Claim("email", user.Email),
            new Claim("role", user.Role.ToString())
        };
        string accessToken = webTokenService.CreateWebToken(claims, applicationSettings.AccessTokenLifetime);
        RefreshToken refreshToken = new(
            cryptographyService.GetRandomString(32),
            clockProvider.Now.AddMinutes(applicationSettings.RefreshTokenLifetime),
            user.Id);
        refreshToken = await refreshTokenRepository.CreateAsync(refreshToken);

        return user.Adapt<AuthRes>(a =>
        {
            a.AccessToken = accessToken;
            a.RefreshToken = refreshToken.Token;
        });
    }
}
