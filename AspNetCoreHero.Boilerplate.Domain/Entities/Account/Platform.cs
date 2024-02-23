public class Platform : AuditableEntity, IAggregateRoot
{
    [MaxLength(50)]
    public string Name { get; set; }
    [MaxLength(10)]
    public string Code { get; set; }
    [MaxLength(500)]
    public string Description { get; set; }
    public virtual List<MenuItem> Menues { get; set; }
}
