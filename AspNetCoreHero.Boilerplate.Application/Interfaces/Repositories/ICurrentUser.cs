using System.Security.Claims;

namespace AspNetCoreHero.Boilerplate.Infrastructure.Identity.Persistence.Interfaces;

public interface ICurrentUser
{
    string? Name { get; }

    Guid GetUserId();

    string? GetUserEmail();

    //string? GetTenant();

    bool IsAuthenticated();

    bool IsInRole(string role);

    IEnumerable<Claim>? GetUserClaims();
}