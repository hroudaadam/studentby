using Studentby.App.Data.Database.Entities.Fields;

namespace Studentby.App.Data.Database.Entities;

public class JobApplication : BaseEntity
{
    public JobApplicationState State { get; set; } = JobApplicationState.Pending;

    public Guid StudentId { get; set; }
    public Guid JobOfferId { get; set; }

    public Student Student { get; set; }
    public JobOffer JobOffer { get; set; }

    public bool IsAlive()
    {
        return State == JobApplicationState.Pending || State == JobApplicationState.Approved;
    }
}
