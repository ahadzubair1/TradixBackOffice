public class Language : AuditableEntity, IAggregateRoot
{
    [MaxLength(250)]
    public string Name { get; set; }
    [MaxLength(2)]
    public string? Alpha2Code { get; set; }
    [MaxLength(3)]
    public string? Alpha3Code { get; set; }

}
