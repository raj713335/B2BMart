namespace B2BMart.DataLayer.Models
{
    public interface IAudit : IAuditCreate, IAuditUpdate
    {

    }

    public interface IAuditUpdate
    {
        string? UpdatedBy { get; set; }
        DateTime? UpdatedAt { get; set; }
    }

    public interface IAuditCreate
    {
        string CreatedBy { get; set; }
        DateTime CreatedAt { get; set; }
    }
}
