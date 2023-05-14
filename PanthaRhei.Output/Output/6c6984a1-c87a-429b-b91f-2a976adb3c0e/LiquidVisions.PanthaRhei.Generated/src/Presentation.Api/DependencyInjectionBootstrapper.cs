using LiquidVisions.PanthaRhei.Generated.Presentation.Api.Controllers;
using LiquidVisions.PanthaRhei.Generated.Application;
using LiquidVisions.PanthaRhei.Generated.Domain;
using LiquidVisions.PanthaRhei.Generated.Infrastructure.EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LiquidVisions.PanthaRhei.Generated.Presentation.Api
{
    internal static class DependencyInjectionBootstrapper
    {
        public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationLayer()
                .AddDomainLayer()
                .AddInfrastructureLayer(configuration.GetConnectionString("DefaultConnectionString"));

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddFieldElements();

            services.AddAppElements();

            services.AddPackageElements();

            services.AddEntityElements();

            services.AddComponentElements();

            services.AddExpanderElements();

            services.AddConnectionStringElements();

            services.AddRelationshipElements();

            return services;
        }

        public static void RunApi(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseFieldEndpoints();

            app.UseAppEndpoints();

            app.UsePackageEndpoints();

            app.UseEntityEndpoints();

            app.UseComponentEndpoints();

            app.UseExpanderEndpoints();

            app.UseConnectionStringEndpoints();

            app.UseRelationshipEndpoints();

            app.Run();
        }
    }
}
