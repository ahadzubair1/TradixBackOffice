[Table("SubscriptionTypes")]
public class SubscriptionType : AuditableEntity, IAggregateRoot
{
    public string Name { get; set; }
    [MaxLength(50)]
    public int SubscriptionId { get; set; }
    public int Duration { get; set; }
    public DurationInterval DurationInterval { get; set; } = DurationInterval.Year;
    [MaxLength(500)]
    public double Amount { get; set; }
    public string Description { get; set; }
}

public enum DurationInterval
{
    Year = 1
}