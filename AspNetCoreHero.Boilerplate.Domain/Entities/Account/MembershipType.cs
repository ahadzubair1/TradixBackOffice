[Table("MembershipTypes")]
public class MembershipType : AuditableEntity, IAggregateRoot
{
    [MaxLength(50)]
    public string Name { get; set; }
    [MaxLength(50)]
    public string Code { get; set; }
    [MaxLength(500)]
    public string? Description { get; set; }
    public string? ImageUri { get; set; }
    public bool HasTiers { get; set; }
    public bool HasCollections { get; set; }
}