using System.Diagnostics.CodeAnalysis;
using LiquidVisions.PanthaRhei.Domain.Usecases;
using LiquidVisions.PanthaRhei.Domain.Usecases.CreateNewExpander;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Initializers;
using LiquidVisions.PanthaRhei.Domain.Usecases.UpdatePackages;
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
        /// <returns>An instance of <seealso cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddDomainLayer(this IServiceCollection services)
        {
            IDependencyManager container = new DependencyManager(new ServiceCollectionWrapper(services));

            return services.AddSingleton(container)
                .AddSingleton((IDependencyFactory)container)
                .AddTransient<IApplication, DotNetApplication>()
                .AddTransient<ICreateNewExpander, CreateNewExpander>()
                .AddTransient<IUpdatePackagesUseCase, UpdatePackagesUseCase>()
                .AddInitializers();
        }

        private static IServiceCollection AddInitializers(this IServiceCollection services)
        {
            return services
                .AddSingleton<IAssemblyManager, AssemblyManager>();
        }
    }
}
