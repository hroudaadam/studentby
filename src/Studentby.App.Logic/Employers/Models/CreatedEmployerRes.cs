namespace Studentby.App.Logic.Employers.Models;

public class CreatedEmployerRes
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string JobTitle { get; set; }
    public string ChangePasswordSecret { get; set; }
}
