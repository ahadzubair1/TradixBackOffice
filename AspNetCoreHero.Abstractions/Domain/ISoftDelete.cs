namespace AspNetCoreHero.Abstractions.Domain;

public interface ISoftDelete
{
    DateTime? DeletedOn { get; set; }
    DefaultIdType? DeletedBy { get; set; }
}