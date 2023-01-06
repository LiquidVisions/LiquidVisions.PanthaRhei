using System.Diagnostics.CodeAnalysis;
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
                .AddTransient<ICommandLine, CommandLine>()
                .AddTransient<IWriter, ClassWriter>();
        }

        private static IServiceCollection AddLogging(this IServiceCollection services)
        {
            services.AddSingleton<ILogManager>(new LogManager());
            services.AddSingleton<ILogger>(new LogManager().Logger);

            return services;
        }
    }
}
