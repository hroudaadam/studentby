namespace Studentby.Shared.Exceptions;

public abstract class NotFoundException : BaseApplicationException
{
    public NotFoundException(string messageKey) : base(messageKey) { }
}
