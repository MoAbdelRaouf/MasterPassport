namespace MasterPassport.Application.DTOs;

public class PersonalInfoDto
{
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
}

public class ContactInfoDto
{
    public string? Address { get; set; }
    public int? CityID { get; set; }
    public string Email { get; set; } = string.Empty;
    public string MobileNumber { get; set; } = string.Empty;
    public string? AlternativeMobile { get; set; }
}

public class WorkPlaceDto
{
    public int? TempWorkPlaceID { get; set; }
    public int WorkPlaceTypeID { get; set; }
    public string JobTitle { get; set; } = string.Empty;
    public string WorkPlaceName { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int? CityID { get; set; }
    public int? UniversityID { get; set; }
    public string? UniversityOtherName { get; set; }
    public int? CollegeID { get; set; }
    public string? CollegeOtherName { get; set; }
    public bool IsCurrent { get; set; }
}

public class ScientificDegreeDto
{
    public int? TempScientificDegreeID { get; set; }
    public int QualificationTypeID { get; set; }
    public int? UniversityID { get; set; }
    public string? UniversityOtherName { get; set; }
    public int? CollegeID { get; set; }
    public string? CollegeOtherName { get; set; }
    public int? QualificationMajorID { get; set; }
    public string? MajorOtherName { get; set; }
    public int? QualificationMinorID { get; set; }
    public string? MinorOtherName { get; set; }
    public DateTime? GraduationDate { get; set; }
    public string? Grade { get; set; }
}

public class UserExperienceFieldDto
{
    public int ExperienceFieldID { get; set; }
    public string? ExperienceFieldName { get; set; }
}

public class UserLanguageDto
{
    public int? TempUserLanguageID { get; set; }
    public int? LanguageID { get; set; }
    public string? LanguageOtherName { get; set; }
    public int ProficiencyLevelID { get; set; }
}

public class UserAttachmentDto
{
    public int? TempUserAttachmentID { get; set; }
    public int AttachmentTypeID { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public int FileSize { get; set; }
    public string ContentType { get; set; } = string.Empty;
}

public class LoginCredentialsDto
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class RegistrationRequestDto
{
    public PersonalInfoDto PersonalInfo { get; set; } = new();
    public ContactInfoDto ContactInfo { get; set; } = new();
    public List<WorkPlaceDto> WorkPlaces { get; set; } = new();
    public List<ScientificDegreeDto> ScientificDegrees { get; set; } = new();
    public List<UserExperienceFieldDto> ExperienceFields { get; set; } = new();
    public List<UserLanguageDto> Languages { get; set; } = new();
    public List<UserAttachmentDto> Attachments { get; set; } = new();
    public LoginCredentialsDto LoginCredentials { get; set; } = new();
}

public class LoginRequestDto
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class LoginResponseDto
{
    public bool Success { get; set; }
    public string Token { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public UserInfoDto? UserInfo { get; set; }
}

public class UserInfoDto
{
    public int UserID { get; set; }
    public string FullNameEn { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public List<string> Roles { get; set; } = new();
}

public class LookupDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public List<string> Errors { get; set; } = new();
}
