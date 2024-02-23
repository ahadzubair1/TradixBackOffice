using Microsoft.Extensions.Logging;
using System.Reflection;

namespace AspNetCoreHero.Boilerplate.Infrastructure.App;

public class RiskTypesSeeder : ICustomSeeder
{
    private readonly ISerializerService _serializerService;
    private readonly ApplicationDbContext _db;
    private readonly ILogger<RiskTypesSeeder> _logger;

    public RiskTypesSeeder(ISerializerService serializerService, ILogger<RiskTypesSeeder> logger, ApplicationDbContext db)
    {
        _serializerService = serializerService;
        _logger = logger;
        _db = db;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        string? path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        if (!_db.RiskTypes.Any())
        {
            _logger.LogInformation("Started to Seed RiskTypes.");

            // Here you can use your own logic to populate the database.
            // As an example, I am using a JSON file to populate the database.
            string data = await File.ReadAllTextAsync(path + "/App/SeedData/RiskTypes.json", cancellationToken);
            var list = _serializerService.Deserialize<List<RiskType>>(data);

            if (list != null)
            {
                foreach (var item in list)
                {
                    await _db.RiskTypes.AddAsync(item, cancellationToken);
                }
            }

            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Seeded RiskTypes.");
        }
    }
}