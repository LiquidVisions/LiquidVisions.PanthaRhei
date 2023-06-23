using System.Diagnostics.CodeAnalysis;
using System.Linq;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Gateways;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors;
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
            GenerationOptions options = services.BuildServiceProvider().GetService<GenerationOptions>();

            services.AddScoped<DbContext, Context>();
            services.AddDbContext<Context>(x =>
            {
                x.UseSqlServer(options.ConnectionString);
                x.UseLazyLoadingProxies();
#if DEBUG
                x.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
                x.EnableSensitiveDataLogging();
#endif
            });

            services.AddTransient<IModelConfiguration, ModelConfiguration>()
                .AddRepositories();

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            string ns = typeof(Entity).Namespace;

            var repositoryType = typeof(GenericRepository<>);
            var getGatewayType = typeof(IGetGateway<>);
            var deleteGatewayType = typeof(IDeleteGateway<>);
            var updateGatewayType = typeof(IUpdateGateway<>);
            var createGatewayType = typeof(ICreateGateway<>);

            var entityTypes = typeof(Entity).Assembly.GetTypes()
                .Where(x => x.IsClass && x.Namespace == ns);

            foreach (var entityType in entityTypes)
            {
                services.AddTransient(getGatewayType.MakeGenericType(entityType), repositoryType.MakeGenericType(entityType));
                services.AddTransient(deleteGatewayType.MakeGenericType(entityType), repositoryType.MakeGenericType(entityType));
                services.AddTransient(updateGatewayType.MakeGenericType(entityType), repositoryType.MakeGenericType(entityType));
                services.AddTransient(createGatewayType.MakeGenericType(entityType), repositoryType.MakeGenericType(entityType));
            }

            return services;
        }
    }
}
