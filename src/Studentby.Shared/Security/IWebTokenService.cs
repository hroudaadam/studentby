using System.Security.Claims;

namespace Studentby.Shared.Security;

public interface IWebTokenService
{
    string CreateWebToken(IEnumerable<Claim> claims, int expiresIn);
    bool IsWebTokenValid(string token);
}