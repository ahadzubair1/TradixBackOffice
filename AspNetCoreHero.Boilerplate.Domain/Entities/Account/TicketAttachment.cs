
public class TicketAttachment : AuditableEntity, IAggregateRoot
{
    public DefaultIdType TicketId { get; set; }
    public string? Path { get; set; }
}
