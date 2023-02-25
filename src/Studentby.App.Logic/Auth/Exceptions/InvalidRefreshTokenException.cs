using Studentby.Shared.Exceptions;

namespace Studentby.App.Logic.Auth.Exceptions;

internal class InvalidRefreshTokenException : BadLogicException
{
    public InvalidRefreshTokenException() : base("InvalidRefreshTokenException")
    {

    }
}
