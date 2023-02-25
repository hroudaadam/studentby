using Studentby.Shared.Exceptions;

namespace Studentby.App.Logic.JobOffers.Exceptions;

internal class JobOfferNotFoundException : NotFoundException
{
    public JobOfferNotFoundException() : base("JobOfferNotFoundException")
    {

    }
}
