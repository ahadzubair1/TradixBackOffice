namespace AspNetCoreHero.Boilerplate.Application.Interfaces;

public class UserDetailsDto
{
    public Guid Id { get; set; }

    public string? UserName { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public bool IsActive { get; set; } = true;

    public bool EmailConfirmed { get; set; }
    public DefaultIdType? CountryId { get; set; }

    public string? PhoneNumber { get; set; }

    public string? ImageUrl { get; set; }

    public NetworkPosition Position { get; set; }
    public NetworkPosition DefaultDownlinePlacementPosition { get; set; }
    public DefaultIdType? NetworkId { get; set; }
    public virtual DefaultIdType? KycId { get; set; }
    public DefaultIdType? ReferredBy { get; set; }
}