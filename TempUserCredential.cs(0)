namespace MasterPassport.Domain.Entities;

public class TempUserCredential : BaseEntity
{
    public int TempUserCredentialID { get; set; }
    public int TempUserID { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string PasswordSalt { get; set; } = string.Empty;

    // Navigation properties
    public virtual TempUser? TempUser { get; set; }
}
