namespace Studentby.App.Data.Database.Entities;

public class RefreshToken : BaseEntity
{
    public string Token { get; set; }
    public bool Revoked { get; set; } = false;
    public DateTime Expiration { get; set; }
    public Guid UserId { get; set; }

    public User User { get; set; }

    public RefreshToken(string token, DateTime expiration, Guid userId)
    {
        Token = token;
        Expiration = expiration;
        UserId = userId;
    }
}
