using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LiquidVisions.PanthaRhei.Application;
using LiquidVisions.PanthaRhei.Application.RequestModels;
using LiquidVisions.PanthaRhei.Infrastructure;
using LiquidVisions.PanthaRhei.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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
            services.AddScoped<IRunSettings, RunSettings>()
                .AddApplicationLayer()
                .AddInfrastructureLayer()
                .AddEntityFrameworkLayer();

            return services;
        }

        /// <summary>
        /// Adds the dependencies of the project to the dependency inversion object.
        /// </summary>
        /// <param name="services"><seealso cref="IServiceCollection"/></param>
        /// <param name="model"><seealso cref="ExpandOptionsRequestModel"/></param>
        /// <returns>An instance of <seealso cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddPresentationLayer(this IServiceCollection services, ExpandOptionsRequestModel model)
        {
            ArgumentNullException.ThrowIfNull(model);

            string full = typeof(Program).Assembly.Location;
            string path = Path.GetDirectoryName(full);

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionStringName = configuration
                .GetSection("ConnectionStrings")
                .GetChildren()
                .Single().Key;

            model.ConnectionString = configuration.GetConnectionString(connectionStringName);

            model.Root = string.IsNullOrEmpty(model.Root)
                ? configuration.GetSection("RunSettings")
                    .GetSection("Root")
                    .Value
                : model.Root;

            model.AppId = model.AppId == Guid.Empty
                ? Guid.Parse(configuration
                    .GetSection("RunSettings")
                    .GetSection("App")
                    .Value)
                : model.AppId;
            
            services.AddApplicationLayer(model)
                .AddInfrastructureLayer()
                .AddEntityFrameworkLayer();

            return services;
        }
    }
}
