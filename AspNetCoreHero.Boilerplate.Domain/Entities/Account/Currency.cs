public class Currency : AuditableEntity, IAggregateRoot
{
    [MaxLength(50)]
    public string Name { get; set; }
    [MaxLength(5)]
    public string Code { get; set; }
    [MaxLength(5)]
    public string Symbol { get; set; }
    public double Price { get; set; }
}
