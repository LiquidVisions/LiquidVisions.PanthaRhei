using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NS.Domain;
using NS.Domain.Entities;
using NS.Application.Gateways;
//-:cnd:noEmit
#if DEBUG
using Microsoft.Extensions.Logging;
#endif
//+:cnd:noEmit

namespace NS.Infrastructure.EntityFramework
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
                //-:cnd:noEmit
#if DEBUG
                options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
                options.EnableSensitiveDataLogging();
#endif
                //+:cnd:noEmit
            });
            return services;
        }
    }
}
