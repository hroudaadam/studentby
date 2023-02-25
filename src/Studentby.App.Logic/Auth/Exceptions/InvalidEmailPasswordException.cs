using Studentby.Shared.Exceptions;

namespace Studentby.App.Logic.Auth.Exceptions;

internal class InvalidEmailPasswordException : BadLogicException
{
    public InvalidEmailPasswordException() : base("InvalidEmailPassword")
    {
    }
}
