
public class TicketComment : AuditableEntity, IAggregateRoot
{
    public DefaultIdType TicketId { get; set; }
    public string? Text { get; set; }
}
