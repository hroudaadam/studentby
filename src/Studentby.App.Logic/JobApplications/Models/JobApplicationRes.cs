using Studentby.App.Data.Database.Entities;
using Studentby.App.Data.Database.Entities.Fields;
using Studentby.App.Logic.JobOffers.Models;

namespace Studentby.App.Logic.JobApplications.Models;

public class JobApplicationRes
{
    public Guid Id { get; set; }
    public JobApplicationState State { get; set; }
    public Guid JobOfferId { get; set; }
    public Guid StudentId { get; set; }
    public JobOfferRes JobOffer { get; set; }
}
