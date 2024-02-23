public class Rank : AuditableEntity, IAggregateRoot
{
    public double LeftPoints { get; set; }
    public double RightPoints { get; set; }
    [MaxLength(50)]
    public string Name { get; set; }
    public int Index { get; set; }
    public int TotalDownlineRanks { get; set; }
    public string Rewards { get; set; } // can this be separated???
    public DefaultIdType? DownlineRankId { get; set; }
    public Rank? DownlineRank { get; set; }
    [MaxLength(250)]
    public string? ImageUri { get; set; }
    public double NetworkCap { get; set; }
    public int RenewalX { get; set; }
}