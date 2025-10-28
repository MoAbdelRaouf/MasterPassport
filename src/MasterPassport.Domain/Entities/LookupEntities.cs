namespace MasterPassport.Domain.Entities;

public class Country : BaseEntity
{
    public int CountryID { get; set; }
    public string CountryNameAr { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CDate { get; set; }

    public virtual ICollection<Governorate> Governorates { get; set; } = new List<Governorate>();
}

public class Governorate : BaseEntity
{
    public int GovernorateID { get; set; }
    public string GovernorateNameAr { get; set; } = string.Empty;
    public int CountryID { get; set; }

    public virtual Country? Country { get; set; }
    public virtual ICollection<City> Cities { get; set; } = new List<City>();
}

public class City : BaseEntity
{
    public int CityID { get; set; }
    public string CityNameAr { get; set; } = string.Empty;
    public int GovernorateID { get; set; }
    public bool IsActive { get; set; }
    public DateTime CDate { get; set; }

    public virtual Governorate? Governorate { get; set; }
}

public class Gender : BaseEntity
{
    public int GenderID { get; set; }
    public string GenderNameAr { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CDate { get; set; }
}

public class Title : BaseEntity
{
    public int TitleID { get; set; }
    public string TitleNameAr { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CDate { get; set; }
}

public class University : BaseEntity
{
    public int UniversityID { get; set; }
    public string UniversityName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CDate { get; set; }

    public virtual ICollection<College> Colleges { get; set; } = new List<College>();
}

public class College : BaseEntity
{
    public int CollegeID { get; set; }
    public int UniversityID { get; set; }
    public string CollegeName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CDate { get; set; }

    public virtual University? University { get; set; }
}

public class WorkPlaceType : BaseEntity
{
    public int WorkPlaceTypeID { get; set; }
    public string TypeName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CDate { get; set; }
}

public class QualificationType : BaseEntity
{
    public int QualificationTypeID { get; set; }
    public string TypeName { get; set; } = string.Empty;
    public bool IsPostgraduate { get; set; }
    public bool IsActive { get; set; }
    public DateTime CDate { get; set; }
}

public class QualificationMajor : BaseEntity
{
    public int QualificationMajorID { get; set; }
    public string MajorName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CDate { get; set; }

    public virtual ICollection<QualificationMinor> QualificationMinors { get; set; } = new List<QualificationMinor>();
}

public class QualificationMinor : BaseEntity
{
    public int QualificationMinorID { get; set; }
    public int QualificationMajorID { get; set; }
    public string MinorName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CDate { get; set; }

    public virtual QualificationMajor? QualificationMajor { get; set; }
}

public class ExperienceCategory : BaseEntity
{
    public int ExperienceCatID { get; set; }
    public string ExperienceCatName { get; set; } = string.Empty;
    public int? ParentID { get; set; }
    public bool IsActive { get; set; }
    public DateTime CDate { get; set; }

    public virtual ExperienceCategory? Parent { get; set; }
    public virtual ICollection<ExperienceCategory> Children { get; set; } = new List<ExperienceCategory>();
    public virtual ICollection<ExperienceField> ExperienceFields { get; set; } = new List<ExperienceField>();
}

public class ExperienceField : BaseEntity
{
    public int ExperienceFieldID { get; set; }
    public int ExperienceCatID { get; set; }
    public string ExperienceFieldName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CDate { get; set; }

    public virtual ExperienceCategory? ExperienceCategory { get; set; }
}

public class Language : BaseEntity
{
    public int LanguageID { get; set; }
    public string LanguageName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CDate { get; set; }
}

public class ProficiencyLevel : BaseEntity
{
    public int ProficiencyLevelID { get; set; }
    public string ProficiencyLevelName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CDate { get; set; }
}

public class AttachmentType : BaseEntity
{
    public int AttachmentTypeID { get; set; }
    public string TypeName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int? MaxFileSizeMB { get; set; }
    public string? AllowedExtensions { get; set; }
    public int AllowedCount { get; set; }
    public bool? IsRequired { get; set; }
    public DateTime? CreatedAt { get; set; }
    public bool IsActive { get; set; }
}

public class Role : BaseEntity
{
    public int RoleID { get; set; }
    public string RoleName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime? CreatedAt { get; set; }
    public bool IsActive { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}

public class InformationSystem : BaseEntity
{
    public int SystemID { get; set; }
    public string SystemName { get; set; } = string.Empty;
    public string SystemCode { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool? IsActive { get; set; }
}

public class ModificationType : BaseEntity
{
    public int ModificationTypeID { get; set; }
    public string ModificationTypeName { get; set; } = string.Empty;
    public string? Description { get; set; }
}
