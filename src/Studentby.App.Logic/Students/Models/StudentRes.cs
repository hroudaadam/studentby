using Studentby.App.Logic.Common.Models;

namespace Studentby.App.Logic.Students.Models;

public class StudentRes
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public bool Activated { get; set; }
    public AddressRes Address { get; set; }
}
