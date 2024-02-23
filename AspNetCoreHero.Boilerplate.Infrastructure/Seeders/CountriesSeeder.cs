using AspNetCoreHero.Abstractions.Interfaces;
using Microsoft.Extensions.Logging;

namespace AspNetCoreHero.Boilerplate.Infrastructure.App;

public class CountriesSeeder : ICustomSeeder
{
    private readonly ISerializerService _serializerService;
    private readonly ApplicationDbContext _db;
    private readonly ILogger<CountriesSeeder> _logger;

    public CountriesSeeder(ISerializerService serializerService, ILogger<CountriesSeeder> logger, ApplicationDbContext db)
    {
        _serializerService = serializerService;
        _logger = logger;
        _db = db;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        string? path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        if (!_db.Countries.Any())
        {
            try
            {
                _logger.LogInformation("Started to Seed Countries.");

                // Here you can use your own logic to populate the database.
                // As an example, I am using a JSON file to populate the database.
                string data = await File.ReadAllTextAsync(path + "/Seeders/SeedData/Countries.json", cancellationToken);
                var list = _serializerService.Deserialize<List<Country>>(data);

                if (list != null)
                {
                    foreach (var item in list)
                    {
                        await _db.Countries.AddAsync(item, cancellationToken);
                    }
                }


                await _db.SaveChangesAsync(cancellationToken);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

            }
            _logger.LogInformation("Seeded Countries.");
        }
    }
}