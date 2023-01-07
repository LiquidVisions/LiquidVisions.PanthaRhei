using System.Diagnostics.CodeAnalysis;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Gateways;
using LiquidVisions.PanthaRhei.Generator.Infrastructure.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LiquidVisions.PanthaRhei.Generator.Infrastructure.EntityFramework
{
    /// <summary>
    /// DependencyInjection extensions for the infrastructure.console library.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class DependencyInjectionExtension
    {
        /// <summary>
        /// Adds the dependencies of the project to the dependency inversion object.
        /// </summary>
        /// <param name="services"><seealso cref="IServiceCollection"/></param>
        /// <returns>An instance of <seealso cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddEntityFrameworkLayer(this IServiceCollection services)
        {
            string connectionString = services.BuildServiceProvider().GetService<Parameters>().ConnectionString;

            services.AddScoped<DbContext, Context>();
            services.AddDbContext<Context>(options =>
            {
                options.UseSqlServer(connectionString);
                options.UseLazyLoadingProxies();
#if DEBUG
                options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
                options.EnableSensitiveDataLogging();
#endif
            });

            services.AddTransient<IGenericGateway<App>, GenericRepository<App>>()
                .AddTransient<IGenericGateway<Expander>, GenericRepository<Expander>>()
                .AddTransient<IGenericGateway<Component>, GenericRepository<Component>>()
                .AddTransient<IGenericGateway<Package>, GenericRepository<Package>>()
                .AddTransient<IGenericGateway<Field>, GenericRepository<Field>>()
                .AddTransient<IGenericGateway<Entity>, GenericRepository<Entity>>()
                .AddTransient<IGenericGateway<Entity>, GenericRepository<Entity>>();

            return services;
        }
    }
}
