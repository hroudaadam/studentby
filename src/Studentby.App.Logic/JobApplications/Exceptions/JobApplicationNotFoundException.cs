using Studentby.Shared.Exceptions;

namespace Studentby.App.Logic.JobApplications.Exceptions;

internal class JobApplicationNotFoundException : NotFoundException
{
    public JobApplicationNotFoundException() : base("JobApplicationNotFoundException")
    {
    }
}
