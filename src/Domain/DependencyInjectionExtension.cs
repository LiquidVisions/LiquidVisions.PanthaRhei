using System.Diagnostics.CodeAnalysis;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Initializers;
using Microsoft.Extensions.DependencyInjection;

namespace LiquidVisions.PanthaRhei.Domain
{
    /// <summary>
    /// DependencyInjection extensions for the domain library.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class DependencyInjectionExtension
    {
        /// <summary>
        /// Adds the dependencies of the project to the dependency inversion object.
        /// </summary>
        /// <param name="services"><seealso cref="IServiceCollection"/></param>
        /// <param name="options"><seealso cref="GenerationOptions"/></param>
        /// <returns>An instance of <seealso cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddDomainLayer(this IServiceCollection services, GenerationOptions options)
        {
            var container = new DependencyManager(services);

            return services.AddSingleton<IDependencyManager>(container)
                .AddSingleton<IDependencyFactory>(container)
                .AddSingleton(options)
                .AddInitializers();
        }

        private static IServiceCollection AddInitializers(this IServiceCollection services)
        {
            return services
                .AddSingleton<IAssemblyManager, AssemblyManager>();
        }
    }
}
