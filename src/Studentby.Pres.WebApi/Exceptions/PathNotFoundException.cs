using Studentby.Shared.Exceptions;

namespace Studentby.Pres.WebApi.Exceptions;

internal class PathNotFoundException : NotFoundException
{
    public PathNotFoundException() : base("PathNotFound")
    {
    }
}
