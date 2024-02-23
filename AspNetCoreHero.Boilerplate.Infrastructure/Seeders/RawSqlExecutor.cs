using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Runtime.CompilerServices;


namespace AspNetCoreHero.Boilerplate.Infrastructure.App;
public class RawSqlExecutor : ICustomSeeder
{
    private readonly ISerializerService _serializerService;
    private readonly ApplicationDbContext _db;
    private readonly ILogger<RawSqlExecutor> _logger;

    public RawSqlExecutor(ISerializerService serializerService, ILogger<RawSqlExecutor> logger, ApplicationDbContext db)
    {
        _serializerService = serializerService;
        _logger = logger;
        _db = db;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        string? path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        _logger.LogInformation("Started to Seed RawSqlSeeder.");

        try
        {
            // Here you can use your own logic to populate the database.
            // As an example, I am using a JSON file to populate the database.
            string rawSql = await File.ReadAllTextAsync(path + "/Seeders/SeedData/UsersNetworkTree.sql", cancellationToken);
            await _db.Database.ExecuteSqlAsync(FormattableStringFactory.Create(rawSql));

            rawSql = await File.ReadAllTextAsync(path + "/Seeders/SeedData/GetUserNetworkFarNode.sql", cancellationToken);
            await _db.Database.ExecuteSqlAsync(FormattableStringFactory.Create(rawSql));

            rawSql = await File.ReadAllTextAsync(path + "/Seeders/SeedData/GetUserDownlineNetworkTree.sql", cancellationToken);
            await _db.Database.ExecuteSqlAsync(FormattableStringFactory.Create(rawSql));

            rawSql = await File.ReadAllTextAsync(path + "/Seeders/SeedData/GetUserUplineNetworkTree.sql", cancellationToken);
            await _db.Database.ExecuteSqlAsync(FormattableStringFactory.Create(rawSql));

			rawSql = await File.ReadAllTextAsync(path + "/Seeders/SeedData/GetUserDownlineNetworkTreeWithDepth.sql", cancellationToken);
			await _db.Database.ExecuteSqlAsync(FormattableStringFactory.Create(rawSql));

            rawSql = await File.ReadAllTextAsync(path + "/Seeders/SeedData/GetUserDashboardData.sql", cancellationToken);
            await _db.Database.ExecuteSqlAsync(FormattableStringFactory.Create(rawSql));


            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Seeded RawSqlSeeder.");
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.ToString());
        }
    }
}