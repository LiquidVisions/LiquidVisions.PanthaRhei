using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LiquidVisions.PanthaRhei.Generated.Domain;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using LiquidVisions.PanthaRhei.Generated.Application.Gateways;
#if DEBUG
using Microsoft.Extensions.Logging;
#endif

namespace LiquidVisions.PanthaRhei.Generated.Infrastructure.EntityFramework
{
    public static class DependencyInjectionBootstrapper
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, string connectionString)
        {
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
            services.AddField();
            services.AddApp();
            services.AddPackage();
            services.AddEntity();
            services.AddComponent();
            services.AddExpander();
            services.AddConnectionString();
            services.AddRelationship();
            services.AddField();
            services.AddApp();
            services.AddPackage();
            services.AddEntity();
            services.AddComponent();
            services.AddExpander();
            services.AddConnectionString();
            services.AddRelationship();
            return services;
        }









        private static IServiceCollection AddField(this IServiceCollection services)
        {
            services.AddTransient<ICreateGateway<Field>, FieldRepository>();
            services.AddTransient<IGetGateway<Field>, FieldRepository>();
            services.AddTransient<IGetByIdGateway<Field>, FieldRepository>();
            services.AddTransient<IDeleteGateway<Field>, FieldRepository>();
            services.AddTransient<IUpdateGateway<Field>, FieldRepository>();

            return services;
        }

        private static IServiceCollection AddApp(this IServiceCollection services)
        {
            services.AddTransient<ICreateGateway<App>, AppRepository>();
            services.AddTransient<IGetGateway<App>, AppRepository>();
            services.AddTransient<IGetByIdGateway<App>, AppRepository>();
            services.AddTransient<IDeleteGateway<App>, AppRepository>();
            services.AddTransient<IUpdateGateway<App>, AppRepository>();

            return services;
        }

        private static IServiceCollection AddPackage(this IServiceCollection services)
        {
            services.AddTransient<ICreateGateway<Package>, PackageRepository>();
            services.AddTransient<IGetGateway<Package>, PackageRepository>();
            services.AddTransient<IGetByIdGateway<Package>, PackageRepository>();
            services.AddTransient<IDeleteGateway<Package>, PackageRepository>();
            services.AddTransient<IUpdateGateway<Package>, PackageRepository>();

            return services;
        }

        private static IServiceCollection AddEntity(this IServiceCollection services)
        {
            services.AddTransient<ICreateGateway<Entity>, EntityRepository>();
            services.AddTransient<IGetGateway<Entity>, EntityRepository>();
            services.AddTransient<IGetByIdGateway<Entity>, EntityRepository>();
            services.AddTransient<IDeleteGateway<Entity>, EntityRepository>();
            services.AddTransient<IUpdateGateway<Entity>, EntityRepository>();

            return services;
        }

        private static IServiceCollection AddComponent(this IServiceCollection services)
        {
            services.AddTransient<ICreateGateway<Component>, ComponentRepository>();
            services.AddTransient<IGetGateway<Component>, ComponentRepository>();
            services.AddTransient<IGetByIdGateway<Component>, ComponentRepository>();
            services.AddTransient<IDeleteGateway<Component>, ComponentRepository>();
            services.AddTransient<IUpdateGateway<Component>, ComponentRepository>();

            return services;
        }

        private static IServiceCollection AddExpander(this IServiceCollection services)
        {
            services.AddTransient<ICreateGateway<Expander>, ExpanderRepository>();
            services.AddTransient<IGetGateway<Expander>, ExpanderRepository>();
            services.AddTransient<IGetByIdGateway<Expander>, ExpanderRepository>();
            services.AddTransient<IDeleteGateway<Expander>, ExpanderRepository>();
            services.AddTransient<IUpdateGateway<Expander>, ExpanderRepository>();

            return services;
        }

        private static IServiceCollection AddConnectionString(this IServiceCollection services)
        {
            services.AddTransient<ICreateGateway<ConnectionString>, ConnectionStringRepository>();
            services.AddTransient<IGetGateway<ConnectionString>, ConnectionStringRepository>();
            services.AddTransient<IGetByIdGateway<ConnectionString>, ConnectionStringRepository>();
            services.AddTransient<IDeleteGateway<ConnectionString>, ConnectionStringRepository>();
            services.AddTransient<IUpdateGateway<ConnectionString>, ConnectionStringRepository>();

            return services;
        }

        private static IServiceCollection AddRelationship(this IServiceCollection services)
        {
            services.AddTransient<ICreateGateway<Relationship>, RelationshipRepository>();
            services.AddTransient<IGetGateway<Relationship>, RelationshipRepository>();
            services.AddTransient<IGetByIdGateway<Relationship>, RelationshipRepository>();
            services.AddTransient<IDeleteGateway<Relationship>, RelationshipRepository>();
            services.AddTransient<IUpdateGateway<Relationship>, RelationshipRepository>();

            return services;
        }
    }
}
