using NS.Presentation.Api.Endpoints;
using NS.Application;
using NS.Domain;
using NS.Infrastructure.EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NS.Presentation.Api
{
    internal static class DependencyInjectionBootstrapper
    {
        public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationLayer()
                .AddDomainLayer()
                .AddInfrastructureLayer(configuration.GetConnectionString("DefaultConnectionString"));

            return services;
        }

        public static void RunApi(this WebApplication app)
        {
            app.Run();
        }
    }
}
