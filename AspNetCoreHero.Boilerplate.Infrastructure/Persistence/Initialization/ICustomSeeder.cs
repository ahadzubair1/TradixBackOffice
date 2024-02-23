using System.Threading;

namespace AspNetCoreHero.Boilerplate.Infrastructure.Identity.Persistence.Initialization;

public interface ICustomSeeder
{
    Task InitializeAsync(CancellationToken cancellationToken);
}