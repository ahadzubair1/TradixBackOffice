using AspNetCoreHero.Boilerplate.Infrastructure.App;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading;

namespace AspNetCoreHero.Boilerplate.Infrastructure.Identity.Persistence.Initialization;

public class CustomSeederRunner
{
    private readonly ICustomSeeder[] _seeders;
    private readonly ILogger<CustomSeederRunner> _logger;

    public CustomSeederRunner(IServiceProvider serviceProvider, ILogger<CustomSeederRunner> logger) =>
        (_seeders, _logger) = (serviceProvider.GetServices<ICustomSeeder>().ToArray(), _logger);

    public async Task RunSeedersAsync(CancellationToken cancellationToken)
    {
        foreach (var seeder in _seeders)
        {
            try
            {
                await seeder.InitializeAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            
        }
    }
}