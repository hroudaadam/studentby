using Studentby.Shared.Exceptions;

namespace Studentby.App.Logic.JobOffers.Exceptions;

internal class JobOfferAlreadyStartedException : BadLogicException
{
    public JobOfferAlreadyStartedException() : base("JobOfferAlreadyStartedException")
    {
    }
}
