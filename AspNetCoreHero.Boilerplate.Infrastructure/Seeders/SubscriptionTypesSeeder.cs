using Microsoft.Extensions.Logging;
using System.Reflection;


namespace AspNetCoreHero.Boilerplate.Infrastructure.App;

public class SubscriptionTypesSeeder : ICustomSeeder
{
    private readonly ISerializerService _serializerService;
    private readonly ApplicationDbContext _db;
    private readonly ILogger<SubscriptionTypesSeeder> _logger;

    public SubscriptionTypesSeeder(ISerializerService serializerService, ILogger<SubscriptionTypesSeeder> logger, ApplicationDbContext db)
    {
        _serializerService = serializerService;
        _logger = logger;
        _db = db;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        string? path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        if (!_db.SubscriptionTypes.Any())
        {
            try
            {
                _logger.LogInformation("Started to Seed SubscriptionTypesSeeder.");

                // Here you can use your own logic to populate the database.
                // As an example, I am using a JSON file to populate the database.
                string data = await File.ReadAllTextAsync(path + "/Seeders/SeedData/SubscriptionTypes.json", cancellationToken);
                var list = _serializerService.Deserialize<List<SubscriptionType>>(data);

                if (list != null)
                {
                    foreach (var item in list)
                    {
                        await _db.SubscriptionTypes.AddAsync(item, cancellationToken);
                    }
                }


                await _db.SaveChangesAsync(cancellationToken);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

            }
            _logger.LogInformation("Seeded SubscriptionTypesSeeder.");

            //_logger.LogInformation("Start Seeding Memberships.");


        }
    }
}