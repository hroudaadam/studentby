using Studentby.Shared.Exceptions;

namespace Studentby.App.Logic.JobOffers.Exceptions;

internal class JobOfferFullException : BadLogicException
{
    public JobOfferFullException() : base("JobOfferFullException")
    {
    }
}
