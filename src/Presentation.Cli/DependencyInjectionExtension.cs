using System;
using System.IO;
using System.Linq;
using LiquidVisions.PanthaRhei.Application;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Infrastructure;
using LiquidVisions.PanthaRhei.Infrastructure.EntityFramework;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LiquidVisions.PanthaRhei.Presentation.Cli
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
        public static IServiceCollection AddPresentationLayer(this IServiceCollection services)
        {
            string full = typeof(Program).Assembly.Location;
            string path = Path.GetDirectoryName(full);

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appsettings.json")
                .Build();

            GenerationOptions options = new();

            string connectionStringName = configuration
                .GetSection("ConnectionStrings")
                .GetChildren()
                .Single().Key;

            options.ConnectionString = configuration.GetConnectionString(connectionStringName);

            options.Root = string.IsNullOrEmpty(options.Root)
                ? configuration.GetSection("RunSettings")
                    .GetSection("Root")
                    .Value
                : options.Root;

            options.AppId = options.AppId == Guid.Empty
                ? Guid.Parse(configuration
                    .GetSection("RunSettings")
                    .GetSection("App")
                    .Value)
                : options.AppId;

            services
                .AddSingleton(options)
                .AddScoped<IRunSettings, RunSettings>()
                .AddApplicationLayer()
                .AddInfrastructureLayer(options.Root)
                .AddEntityFrameworkLayer(options.ConnectionString);
            
            return services;
        }
    }
}
