using Studentby.Shared.Exceptions;

namespace Studentby.App.Logic.JobApplications.Exceptions;
internal class JobApplicationAlreadyExistsException : BadLogicException
{
    public JobApplicationAlreadyExistsException() : base("JobApplicationAlreadyExistsException")
    {
    }
}
