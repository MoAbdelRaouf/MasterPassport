namespace MasterPassport.Domain.Entities;

public class PasswordHistory : BaseEntity
{
    public int HistoryID { get; set; }
    public int UserID { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
    public string PasswordSalt { get; set; } = string.Empty;
    public DateTime ChangedAt { get; set; }
    public string ChangedBy { get; set; } = string.Empty;

    // Navigation properties
    public virtual User? User { get; set; }
}
