namespace Studentby.App.Data.Database.Entities;

public class Employer : BaseEntity
{
    public string JobTitle { get; set; }
    public Guid GroupId { get; set; }
    public Guid UserId { get; set; }

    public Group Group { get; set; }
    public User User { get; set; }
}
