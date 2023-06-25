using System.Diagnostics.CodeAnalysis;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Gateways;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Logging;
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
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
        {
            return services.AddLogging()
                .AddSingleton<IFile>(new FileService())
                .AddSingleton<IDirectory>(new DirectoryService())
                .AddTransient<ICommandLineInteractor, CommandLine>()
                .AddTransient<IWriterInteractor, ClassWriter>()
                .AddTransient<IHarvestSerializerInteractor, HarvestSerializerInteractor>()
                .AddTransient<IDeserializerInteractor<Harvest>, SerializerInteractor<Harvest>>()
                .AddTransient<ISerializerInteractor<Harvest>, SerializerInteractor<Harvest>>()
                .AddTransient<IGetGateway<Harvest>, HarvestRepository>()
                .AddTransient<ICreateGateway<Harvest>, HarvestRepository>();
        }

        private static IServiceCollection AddLogging(this IServiceCollection services)
        {
            GenerationOptions requestModel = services.BuildServiceProvider().GetService<GenerationOptions>();
            var logManager = new LogManager(requestModel);

            services.AddSingleton<ILogManager>(logManager);
            services.AddSingleton<ILogger>(logManager.Logger);

            return services;
        }
    }
}
