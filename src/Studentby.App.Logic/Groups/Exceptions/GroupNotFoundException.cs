using Studentby.Shared.Exceptions;

namespace Studentby.App.Logic.Groups.Exceptions;

internal class GroupNotFoundException : NotFoundException
{
    public GroupNotFoundException() : base("GroupNotFoundException")
    {

    }
}
