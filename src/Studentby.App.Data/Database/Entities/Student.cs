namespace Studentby.App.Data.Database.Entities;

public class Student : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public bool Activated { get; set; } = false;
    public Guid? AddressId { get; set; }
    public Guid UserId { get; set; }

    public Address Address { get; set; }
    public User User { get; set; }
    public IEnumerable<JobApplication> JobApplications { get; set; }
}
