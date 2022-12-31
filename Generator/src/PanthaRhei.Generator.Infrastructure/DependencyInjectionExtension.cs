using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;
using LiquidVisions.PanthaRhei.Generator.Domain.Logging;
using LiquidVisions.PanthaRhei.Generator.Infrastructure.IO;
using LiquidVisions.PanthaRhei.Generator.Infrastructure.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace LiquidVisions.PanthaRhei.Generator.Infrastructure
{
    /// <summary>
    /// DependencyInjection extensions for the infrastructure library.
    /// </summary>
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
                .AddSingleton<IFileService>(new FileService())
                .AddSingleton<IDirectoryService>(new DirectoryService())
                .AddTransient<ICommandLine, CommandLine>();
        }

        private static IServiceCollection AddLogging(this IServiceCollection services)
        {
            services.AddSingleton<ILogManager>(new LogManager());
            services.AddSingleton<ILogger>(new LogManager().Logger);

            return services;
        }
    }
}
