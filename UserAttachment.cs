namespace MasterPassport.Domain.Entities;

public class UserAttachment : BaseEntity
{
    public int UserAttachmentID { get; set; }
    public int UserID { get; set; }
    public int AttachmentTypeID { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public int FileSize { get; set; }
    public string ContentType { get; set; } = string.Empty;
    public DateTime UploadedAt { get; set; }

    // Navigation properties
    public virtual User? User { get; set; }
    public virtual AttachmentType? AttachmentType { get; set; }
}
