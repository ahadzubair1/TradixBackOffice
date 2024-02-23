namespace VM.WebApi.Domain.App;

[Table("ReferralCodes")]
public class ReferralCode : AuditableEntity, IAggregateRoot
{
    public DefaultIdType UserId { get; set; }
    public string Code { get; set; }
    public NetworkPosition NetworkPosition { get; set; }
}