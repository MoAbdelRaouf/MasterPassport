namespace MasterPassport.Domain.Entities;

public class UserCredential : BaseEntity
{
    public int UserCredentialID { get; set; }
    public int UserID { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string PasswordSalt { get; set; } = string.Empty;

    // Navigation properties
    public virtual User? User { get; set; }
}
