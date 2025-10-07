namespace MasterPassport.Domain.Entities;

public class TempUserLanguage : BaseEntity
{
    public int TempUserLanguageID { get; set; }
    public int TempUserID { get; set; }
    public int? LanguageID { get; set; }
    public string? LanguageOtherName { get; set; }
    public int ProficiencyLevelID { get; set; }

    // Navigation properties
    public virtual TempUser? TempUser { get; set; }
    public virtual Language? Language { get; set; }
    public virtual ProficiencyLevel? ProficiencyLevel { get; set; }
}
