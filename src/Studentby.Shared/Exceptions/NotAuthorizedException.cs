namespace Studentby.Shared.Exceptions;

public class NotAuthorizedException : BaseApplicationException
{
    public NotAuthorizedException() : base("NotAuthorized") { }
}
