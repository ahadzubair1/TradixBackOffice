public class TicketType : AuditableEntity, IAggregateRoot
{
    [MaxLength(100)]
    public string Name { get; set; }
    [MaxLength(50)]
    public string Code { get; set; }
    [MaxLength(250)]
    public string? Description { get; set; }
}
