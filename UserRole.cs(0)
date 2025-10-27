namespace MasterPassport.Domain.Entities;

public class UserRole : BaseEntity
{
    public int UserRoleID { get; set; }
    public int UserID { get; set; }
    public int RoleID { get; set; }
    public DateTime AssignedAt { get; set; }

    // Navigation properties
    public virtual User? User { get; set; }
    public virtual Role? Role { get; set; }
}
