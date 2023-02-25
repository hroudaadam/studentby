using Studentby.Shared.Exceptions;

namespace Studentby.App.Logic.Users.Exceptions;

internal class UserEmailAlreadyTakenException : BadLogicException
{
    public UserEmailAlreadyTakenException() : base("UserEmailAlreadyTakenException")
    {
    }
}