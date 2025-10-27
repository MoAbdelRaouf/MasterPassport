namespace MasterPassport.Domain.Entities;

public class UserLanguage : BaseEntity
{
    public int UserLanguageID { get; set; }
    public int UserID { get; set; }
    public int? LanguageID { get; set; }
    public string? LanguageOtherName { get; set; }
    public int ProficiencyLevelID { get; set; }

    // Navigation properties
    public virtual User? User { get; set; }
    public virtual Language? Language { get; set; }
    public virtual ProficiencyLevel? ProficiencyLevel { get; set; }
}
