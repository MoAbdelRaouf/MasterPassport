namespace MasterPassport.Domain.Entities;

public class WorkPlace : BaseEntity
{
    public int WorkPlaceID { get; set; }
    public int UserID { get; set; }
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

    // Navigation properties
    public virtual User? User { get; set; }
    public virtual WorkPlaceType? WorkPlaceType { get; set; }
    public virtual City? City { get; set; }
    public virtual University? University { get; set; }
    public virtual College? College { get; set; }
}
