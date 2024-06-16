using System;
using System.IO;
using System.Linq;
using LiquidVisions.PanthaRhei.Application;
using LiquidVisions.PanthaRhei.Application.RequestModels;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators;
using LiquidVisions.PanthaRhei.Infrastructure;
using LiquidVisions.PanthaRhei.Infrastructure.EntityFramework;
using LiquidVisions.PanthaRhei.Presentation.Cli.UseCases;
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
            services.AddScoped<ICommand<SetDatabaseCommandModel>, SetDatabaseUseCase>()
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

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<Program>()
                .Build();

            string connectionStringName = configuration
                .GetSection("ConnectionStrings")
                .GetChildren()
                .Single().Key;

            if(string.IsNullOrEmpty(model.Root))
            {
                string root = configuration.GetSection("RunSettings")
                    .GetSection("Root")
                    .Value;

                model.Root = root;
            }

            model.ConnectionString = configuration.GetConnectionString(connectionStringName);

            services.AddApplicationLayer(model)
                .AddInfrastructureLayer()
                .AddEntityFrameworkLayer();

            return services;
        }
    }
}
