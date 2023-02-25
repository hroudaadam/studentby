using Studentby.App.Data.Database.Entities.Fields;

namespace Studentby.App.Data.Database.Entities;

public class JobOffer : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public double Wage { get; set; }
    public int Spaces { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public Guid? AddressId { get; set; }
    public Guid GroupId { get; set; }

    public Address Address { get; set; }
    public Group Group { get; set; }
    public ICollection<JobApplication> JobApplications { get; set; }

    public int CountFreeSpaces()
    {
        if (JobApplications == null) return Spaces;

        int occupied = JobApplications.Count(ja => ja.State == JobApplicationState.Approved);
        return Spaces - occupied;
    }
}
