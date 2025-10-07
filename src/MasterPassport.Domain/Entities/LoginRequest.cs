namespace MasterPassport.Domain.Entities;

public class LoginRequest : BaseEntity
{
    public int RequestID { get; set; }
    public int? UserID { get; set; }
    public string Username { get; set; } = string.Empty;
    public int SystemID { get; set; }
    public DateTime? RequestDate { get; set; }
    public string IPAddress { get; set; } = string.Empty;
    public string? UserAgent { get; set; }
    public bool IsSuccessful { get; set; }
    public string? FailureReason { get; set; }
    public string? SessionID { get; set; }

    // Navigation properties
    public virtual User? User { get; set; }
    public virtual InformationSystem? InformationSystem { get; set; }
}
