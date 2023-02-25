using Studentby.App.Data.Database.Entities;
using Studentby.App.Data.Database.Entities.Fields;
using Studentby.App.Logic.JobOffers.Models;
using Studentby.App.Logic.Students.Models;

namespace Studentby.App.Logic.JobApplications.Models;

public class JobApplicationDetailRes
{
    public Guid Id { get; set; }
    public JobApplicationState State { get; set; }
    public Guid JobOfferId { get; set; }
    public Guid StudentId { get; set; }
    public StudentRes Student { get; set; }
    public JobOfferDetailRes JobOffer { get; set; }
}
