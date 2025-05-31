namespace CmsModels;
public abstract class SyncEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string? CreatedBy { get; set; }
    public DateTime LastModified { get; set; } = DateTime.UtcNow;
    public string? ModifiedBy { get; set; }
    public bool IsActive { get; set; } = true;
    public bool NeedsSync { get; set; } = true;
}