namespace MasterPassport.Domain.Entities;

public class TempUserExperienceField : BaseEntity
{
    public int TempUserExperienceFieldID { get; set; }
    public int TempUserID { get; set; }
    public int ExperienceFieldID { get; set; }

    // Navigation properties
    public virtual TempUser? TempUser { get; set; }
    public virtual ExperienceField? ExperienceField { get; set; }
}
