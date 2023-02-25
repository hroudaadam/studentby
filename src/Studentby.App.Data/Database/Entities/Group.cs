namespace Studentby.App.Data.Database.Entities;

public class Group : BaseEntity
{
    public string Name { get; set; }

    public ICollection<Employer> Employers { get; set; }
    public ICollection<JobOffer> JobOffers { get; set; }
}
