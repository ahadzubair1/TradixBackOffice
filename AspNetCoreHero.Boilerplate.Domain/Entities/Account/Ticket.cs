

public class Ticket : AuditableEntity, IAggregateRoot
{
    public DefaultIdType UserId { get; set; }
    public DefaultIdType? TicketTypeId { get; set; }
    public virtual TicketType? TicketType { get; set; }
    public virtual List<TicketAttachment>? Attachments { get; set; }
    public virtual List<TicketComment>? Comments { get; set; }
    [MaxLength(50)]
    public string Code { get; set; } = default!;
    [MaxLength(100)]
    public string? Subject { get; set; }
    [MaxLength(4000)]
    public string? Description { get; set; }
    //public TicketStatus TicketStatus { get; set; } = TicketStatus.UnAssigned;
    public DefaultIdType? AssignedTo { get; set; }
}
