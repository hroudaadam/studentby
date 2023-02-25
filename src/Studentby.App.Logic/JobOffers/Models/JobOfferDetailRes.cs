using Studentby.App.Logic.Common.Models;
using Studentby.App.Logic.Groups.Models;

namespace Studentby.App.Logic.JobOffers.Models;

public class JobOfferDetailRes
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public double Wage { get; set; }
    public int Spaces { get; set; }
    public int FreeSpaces { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public Guid AddressId { get; set; }
    public Guid GroupId { get; set; }
    public AddressRes Address { get; set; }
    public GroupRes Group { get; set; }
}
