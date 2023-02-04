using System.Diagnostics.CodeAnalysis;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Gateways;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Harvesters;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Initializers;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Serialization;
using Microsoft.Extensions.DependencyInjection;

namespace LiquidVisions.PanthaRhei.Generator.Domain
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
            var container = new DependencyManagerInteractor(services);

            return services.AddSingleton<IDependencyManagerInteractor>(container)
                .AddSingleton<IDependencyFactoryInteractor>(container)
                .AddSingleton(new Parameters())
                .AddInitializers()
                .AddTransient<IHarvestSerializerInteractor, HarvestSerializerInteractor>();
        }

        private static IServiceCollection AddInitializers(this IServiceCollection services)
        {
            return services
                .AddTransient<ISerializerInteractor<Harvest>, SerializerInteractor<Harvest>>()
                .AddTransient<IDeserializerInteractor<Harvest>, SerializerInteractor<Harvest>>()
                .AddSingleton<IAssemblyManagerInteractor, AssemblyManagerInteractor>();
        }
    }
}
