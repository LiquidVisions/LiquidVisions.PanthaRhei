using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Repositories;
using LiquidVisions.PanthaRhei.Domain.Usecases;
using LiquidVisions.PanthaRhei.Infrastructure.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LiquidVisions.PanthaRhei.Infrastructure.EntityFramework
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
        /// <param name="connectionString">the connectionstring to the data store.</param>
        /// <returns>An instance of <seealso cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddEntityFrameworkLayer(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<DbContext, Context>();
            services.AddDbContext<Context>(x =>
            {
                x.UseSqlServer(connectionString);
                x.UseLazyLoadingProxies();
#if DEBUG
                x.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
                x.EnableSensitiveDataLogging();
#endif
            });

            services.AddTransient<IModelConfiguration, ModelConfiguration>()
                .AddTransient<IMigrationService, GenericRepository>()
                .AddRepositories();

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            string ns = typeof(Entity).Namespace;

            Type repositoryType = typeof(GenericRepository<>);
            Type getGatewayType = typeof(IGetRepository<>);
            Type deleteGatewayType = typeof(IDeleteRepository<>);
            Type updateGatewayType = typeof(IUpdateRepository<>);
            Type createGatewayType = typeof(ICreateRepository<>);

            IEnumerable<Type> entityTypes = typeof(Entity).Assembly.GetTypes()
                .Where(x => x.IsClass && x.Namespace == ns);

            foreach (Type entityType in entityTypes)
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
