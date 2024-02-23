public class BonusType : AuditableEntity, IAggregateRoot
{
    public string Name { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public double Percentage { get; set; }
    public double Cap { get; set; }
    public BonusIntervalTypes IntervalType { get; set; }
    public bool IsActive { get; set; }
}