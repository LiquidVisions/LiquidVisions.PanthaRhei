using System.Diagnostics.CodeAnalysis;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Logging;
using LiquidVisions.PanthaRhei.Domain.Repositories;
using LiquidVisions.PanthaRhei.Domain.Usecases;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Harvesters;
using LiquidVisions.PanthaRhei.Infrastructure.IO;
using LiquidVisions.PanthaRhei.Infrastructure.Logging;
using LiquidVisions.PanthaRhei.Infrastructure.Serialization;
using Microsoft.Extensions.DependencyInjection;

namespace LiquidVisions.PanthaRhei.Infrastructure
{
    /// <summary>
    /// DependencyInjection extensions for the infrastructure library.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class DependencyInjectionExtension
    {
        /// <summary>
        /// Adds the dependencies of the project to the dependency inversion object.
        /// </summary>
        /// <param name="services"><seealso cref="IServiceCollection"/></param>
        /// <returns>An instance of <seealso cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, string logPath)
        {
            return services.AddLogging(logPath)
                .AddSingleton<IFile>(new FileService())
                .AddSingleton<IDirectory>(new DirectoryService())
                .AddTransient<ICommandLine, CommandLine>()
                .AddTransient<IWriter, ClassWriter>()
                .AddTransient<IHarvestSerializer, HarvestSerializer>()
                .AddTransient<IDeserializer<Harvest>, Serializer<Harvest>>()
                .AddTransient<ISerializer<Harvest>, Serializer<Harvest>>()
                .AddTransient<IGetRepository<Harvest>, HarvestRepository>()
                .AddTransient<ICreateRepository<Harvest>, HarvestRepository>();
        }

        private static IServiceCollection AddLogging(this IServiceCollection services, string logPath)
        {
            LogManager logManager = new(logPath);

            services.AddSingleton<ILogManager>(logManager);
            services.AddSingleton<ILogger>(logManager.Logger);

            return services;
        }
    }
}
