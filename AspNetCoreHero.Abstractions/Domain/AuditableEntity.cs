using AspNetCoreHero.Abstractions.Domain;
using System.ComponentModel.DataAnnotations;
public enum Status
{
    Unknown = 0,
    Active = 1,
    InActive = 2,
    Deleted = 3,
    Error = 4,
    Warning = 5
}
public abstract class AuditableEntity : AuditableEntity<DefaultIdType>
{
}

public abstract class AuditableEntity<T> : BaseEntity<T>, IAuditableEntity, ISoftDelete
{
    public Guid CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public Guid LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    public DateTime? DeletedOn { get; set; }
    public Guid? DeletedBy { get; set; }

    public Status Status { get; set; } = Status.Active;
    [Timestamp]
    public byte[] RowVersion { get; set; }
    protected AuditableEntity()
    {
        CreatedOn = DateTime.UtcNow;
        LastModifiedOn = DateTime.UtcNow;
    }
}
