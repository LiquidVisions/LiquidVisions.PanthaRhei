using System.Diagnostics.CodeAnalysis;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Harvesters;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Serialization;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Templates;
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
                .AddTransient<IPluralizer, CustomPluralizer>()
                .AddSingleton<IProjectAgentInteractor, ProjectAgent>()
                .AddInitializers()
                .AddTemplateServices();
        }

        private static IServiceCollection AddTemplateServices(this IServiceCollection services)
        {
            services.AddTransient<ITemplateService, ScribanTemplateService>()
                .AddTransient<ITemplateLoader, TemplateLoader>();

            return services;
        }

        private static IServiceCollection AddInitializers(this IServiceCollection services)
        {
            return services
                .AddTransient<ISerializerInteractor<Harvest>, SerializerInteractor<Harvest>>()
                .AddTransient<IDeserializerInteractor<Harvest>, SerializerInteractor<Harvest>>();
        }
    }
}
