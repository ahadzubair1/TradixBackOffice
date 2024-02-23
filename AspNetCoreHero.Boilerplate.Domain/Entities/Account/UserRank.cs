namespace VM.WebApi.Domain.App;

public class UserRank : AuditableEntity, IAggregateRoot
{
    public DefaultIdType UserId { get; set; }
    public DefaultIdType RankId { get; set; }
    public virtual Rank Rank { get; set; } = default!;
}