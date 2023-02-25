using Studentby.Shared.Exceptions;

namespace Studentby.App.Logic.JobApplications.Exceptions;

internal class InvalidJobApplicationStateException : BadLogicException
{
    public InvalidJobApplicationStateException() : base("InvalidJobApplicationStateException")
    {
    }
}
