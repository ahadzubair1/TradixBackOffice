using AspNetCoreHero.Abstractions.Domain;
using AspNetCoreHero.Abstractions.Repository;
using AspNetCoreHero.Boilerplate.Application.Interfaces.Contexts;
using AspNetCoreHero.Boilerplate.Infrastructure.CacheRepositories;
using AspNetCoreHero.Boilerplate.Infrastructure.DbContexts;
using AspNetCoreHero.Boilerplate.Infrastructure.Identity.Persistence.Interfaces;
using AspNetCoreHero.Boilerplate.Infrastructure.Identity.Persistence.Repository;
using AspNetCoreHero.Boilerplate.Infrastructure.Repositories;
using AspNetCoreHero.Boilerplate.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace AspNetCoreHero.Boilerplate.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPersistenceContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            #region Repositories

            // Add Repositories
            services.AddScoped(typeof(Abstractions.Repository.IRepository<>), typeof(ApplicationDbRepository<>));

            foreach (var aggregateRootType in
                typeof(IAggregateRoot).Assembly.GetExportedTypes()
                    .Where(t => typeof(IAggregateRoot).IsAssignableFrom(t) && t.IsClass)
                    .ToList())
            {
                // Add ReadRepositories.
                services.AddScoped(typeof(IReadRepository<>).MakeGenericType(aggregateRootType), sp =>
                    sp.GetRequiredService(typeof(Abstractions.Repository.IRepository<>).MakeGenericType(aggregateRootType)));
            }
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ITicketRepository, TicketRepository>();
            services.AddTransient<IProductCacheRepository, ProductCacheRepository>();
            services.AddTransient<IBrandRepository, BrandRepository>();
            services.AddTransient<IBrandCacheRepository, BrandCacheRepository>();
            services.AddTransient<ILogRepository, LogRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ISerializerService, NewtonsoftService>();
            services.AddTransient<ITransactionRepository, TransactionRepository>();
            services.AddTransient<IReferralService, ReferralService>();
            services.AddTransient<IEventsRepository, EventsRepository>();
            services.AddTransient<IPurchasedSubscriptionRepository, PurchasedSubscriptionRepository>();

            services.AddServices();

            //services.AddTransient<IDatabaseInitializer, DatabaseInitializer>()
            //   .AddTransient<ApplicationDbInitializer>()
            //   .AddTransient<ApplicationDbSeeder>()
            services.AddServices(typeof(ICustomSeeder), ServiceLifetime.Transient)
               .AddTransient<CustomSeederRunner>();

            #endregion Repositories
        }

        internal static IServiceCollection AddServices(this IServiceCollection services) =>
        services
            .AddServices(typeof(ITransientService), ServiceLifetime.Transient)
            .AddServices(typeof(IScopedService), ServiceLifetime.Scoped);

        internal static IServiceCollection AddServices(this IServiceCollection services, Type interfaceType, ServiceLifetime lifetime)
        {
            var interfaceTypes =
                AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(s => s.GetTypes())
                    .Where(t => interfaceType.IsAssignableFrom(t)
                                && t.IsClass && !t.IsAbstract)
                    .Select(t => new
                    {
                        Service = t.GetInterfaces().FirstOrDefault(),
                        Implementation = t
                    })
                    .Where(t => t.Service is not null
                                && interfaceType.IsAssignableFrom(t.Service));

            foreach (var type in interfaceTypes)
            {
                services.AddService(type.Service!, type.Implementation, lifetime);
            }

            return services;
        }

        internal static IServiceCollection AddService(this IServiceCollection services, Type serviceType, Type implementationType, ServiceLifetime lifetime) =>
            lifetime switch
            {
                ServiceLifetime.Transient => services.AddTransient(serviceType, implementationType),
                ServiceLifetime.Scoped => services.AddScoped(serviceType, implementationType),
                ServiceLifetime.Singleton => services.AddSingleton(serviceType, implementationType),
                _ => throw new ArgumentException("Invalid lifeTime", nameof(lifetime))
            };

    }
}