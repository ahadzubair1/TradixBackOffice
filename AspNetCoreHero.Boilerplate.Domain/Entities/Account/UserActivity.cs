public class UserActivity : AuditableEntity, IAggregateRoot
{
    public DefaultIdType UserId { get; set; }
    public string? UserName { get; set; }
    public string? ActivityType { get; set; }
    public DateTime? ActivityTime { get; set; }
    public string? IPAddress { get; set; }
    public string? UserAgent { get; set; }

}
