using Studentby.App.Data.Database.Entities;
using Studentby.App.Logic.Groups.Models;

namespace Studentby.App.Logic.Employers.Models;

public class EmployerRes
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string JobTitle { get; set; }
    public GroupRes Group { get; set; }
}
