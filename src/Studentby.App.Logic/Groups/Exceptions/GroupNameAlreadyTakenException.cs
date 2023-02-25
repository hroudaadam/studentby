using Studentby.Shared.Exceptions;

namespace Studentby.App.Logic.Groups.Exceptions;

internal class GroupNameAlreadyTakenException : BadLogicException
{
    public GroupNameAlreadyTakenException() : base("GroupNameAlreadyTakenException")
    {

    }
}
