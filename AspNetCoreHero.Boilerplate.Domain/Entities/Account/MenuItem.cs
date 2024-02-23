public class MenuItem : AuditableEntity, IAggregateRoot
{
    public DefaultIdType PlatformId { get; set; }
    [MaxLength(50)]
    public string Title { get; set; }
    public string Controller { get; set; }
    public string Action { get; set; }
    public string Icon { get; set; }
    public DefaultIdType? ParentMenuItemId { get; set; }
    public virtual MenuItem? ParentMenuItem { get; set; }
}