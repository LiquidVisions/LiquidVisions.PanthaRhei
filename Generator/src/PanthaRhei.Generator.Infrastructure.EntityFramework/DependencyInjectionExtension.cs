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
    public static class DependencyInjectionExtension
    {
        /// <summary>
        /// Adds the dependencies of the project to the dependency inversion object.
        /// </summary>
        /// <param name="services"><seealso cref="IServiceCollection"/></param>
        /// <returns>An instance of <seealso cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddEntityFrameworkLayer(this IServiceCollection services)
        {
            services.AddScoped<DbContext, Context>();
            services.AddDbContext<Context>(options =>
            {
                options.UseSqlServer(); // TODO: Inject connectionstring here.
                options.UseLazyLoadingProxies();
#if DEBUG
                options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
                options.EnableSensitiveDataLogging();
#endif
            });

            services.AddTransient<IAppRepository, AppRepository>();

            return services;
        }
    }
}
