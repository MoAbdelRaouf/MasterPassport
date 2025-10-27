namespace MasterPassport.Domain.Entities;

public class ScientificDegree : BaseEntity
{
    public int ScientificDegreeID { get; set; }
    public int UserID { get; set; }
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

    // Navigation properties
    public virtual User? User { get; set; }
    public virtual QualificationType? QualificationType { get; set; }
    public virtual University? University { get; set; }
    public virtual College? College { get; set; }
    public virtual QualificationMajor? QualificationMajor { get; set; }
    public virtual QualificationMinor? QualificationMinor { get; set; }
}
