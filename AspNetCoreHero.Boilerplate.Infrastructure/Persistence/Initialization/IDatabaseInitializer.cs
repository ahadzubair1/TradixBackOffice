using System.Threading;

namespace AspNetCoreHero.Boilerplate.Infrastructure.Identity.Persistence.Initialization;

internal interface IDatabaseInitializer
{
    Task InitializeDatabasesAsync(CancellationToken cancellationToken);
    Task InitializeApplicationDbAsync(CancellationToken cancellationToken);
}