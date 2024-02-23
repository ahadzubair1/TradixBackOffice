using AspNetCoreHero.Boilerplate.Infrastructure.DbContexts;
using AspNetCoreHero.Boilerplate.Infrastructure.Identity;
using AspNetCoreHero.Boilerplate.Infrastructure.Identity.Models;
using AspNetCoreHero.Boilerplate.Infrastructure.Identity.Persistence.Initialization;
using AspNetCoreHero.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Web
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger("app");
                try
                {
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    //userManager.PasswordHasher = new CustomPasswordHasher();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                    var seederRunner = services.GetRequiredService<CustomSeederRunner>();

                    var dbContext = services.GetRequiredService<ApplicationDbContext>();

                    dbContext.Database.Migrate();
                    await Infrastructure.Identity.Seeds.DefaultRoles.SeedAsync(userManager, roleManager);
                    await Infrastructure.Identity.Seeds.DefaultSuperAdminUser.SeedAsync(userManager, roleManager, dbContext);
                    await Infrastructure.Identity.Seeds.DefaultBasicUser.SeedAsync(userManager, roleManager, dbContext);

                    await seederRunner.RunSeedersAsync(default);


                    logger.LogInformation("Finished Seeding Default Data");
                    logger.LogInformation("Application Starting");
                }
                catch (Exception ex)
                {
                    logger.LogWarning(ex, "An error occurred seeding the DB");
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}