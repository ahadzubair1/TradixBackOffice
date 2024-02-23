public class Country : AuditableEntity, IAggregateRoot
{
    [MaxLength(250)]
    public string? Name { get; set; }
    [MaxLength(2)]
    public string? Alpha2Code { get; set; }
    [MaxLength(3)]
    public string? Alpha3Code { get; set; }
    [MaxLength(5)]
    public string? DialingCode { get; set; }
    [MaxLength(250)]
    public string? Flag { get; set; }
    public DefaultIdType? RiskTypeId { get; set; }
    //public RiskType? RiskType { get; set; }
    public bool AllowUserRegistration { get; set; } = true;
}
