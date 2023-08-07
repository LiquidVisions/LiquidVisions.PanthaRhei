using NS.Presentation.Api.Controllers;
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
                .AddInfrastructureLayer(configuration.GetConnectionString("CONNECTION_STRING_PLACEHOLDER"));

            services.AddAutoMapper(typeof(Program));

            return services;
        }

        public static void RunApi(this WebApplication app)
        {
            app.Run();
        }
    }
}
