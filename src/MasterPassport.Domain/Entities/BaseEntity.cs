namespace MasterPassport.Domain.Entities;

public abstract class BaseEntity
{
    public DateTime CDate { get; set; }
    public int CUser { get; set; }
    public DateTime? EDate { get; set; }
    public int? EUser { get; set; }
    public bool IsActive { get; set; }
}
