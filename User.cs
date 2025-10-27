namespace MasterPassport.Domain.Entities;

public class User : BaseEntity
{
    public int UserID { get; set; }
    public int NationalityID { get; set; }
    public string? NationalID { get; set; }
    public string? PassportNumber { get; set; }
    public int TitleID { get; set; }
    public string FirstNameAr { get; set; } = string.Empty;
    public string SecondNameAr { get; set; } = string.Empty;
    public string ThirdNameAr { get; set; } = string.Empty;
    public string FourthNameAr { get; set; } = string.Empty;
    public string FullNameEn { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public int GenderID { get; set; }
    public string? PersonalImage { get; set; }
    public string? Address { get; set; }
    public int? CityID { get; set; }
    public string Email { get; set; } = string.Empty;
    public string MobileNumber { get; set; } = string.Empty;
    public string? AlternativeMobile { get; set; }
    public DateTime RegistrationDate { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    public virtual Country? Nationality { get; set; }
    public virtual Title? Title { get; set; }
    public virtual Gender? Gender { get; set; }
    public virtual City? City { get; set; }
    public virtual ICollection<WorkPlace> WorkPlaces { get; set; } = new List<WorkPlace>();
    public virtual ICollection<ScientificDegree> ScientificDegrees { get; set; } = new List<ScientificDegree>();
    public virtual ICollection<UserExperienceField> UserExperienceFields { get; set; } = new List<UserExperienceField>();
    public virtual ICollection<UserLanguage> UserLanguages { get; set; } = new List<UserLanguage>();
    public virtual ICollection<UserAttachment> UserAttachments { get; set; } = new List<UserAttachment>();
    public virtual UserCredential? UserCredential { get; set; }
    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
