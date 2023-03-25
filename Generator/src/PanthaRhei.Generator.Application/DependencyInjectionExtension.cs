using System.Diagnostics.CodeAnalysis;
using LiquidVisions.PanthaRhei.Generator.Application.Boundaries;
using LiquidVisions.PanthaRhei.Generator.Application.Interactors.Gateways;
using LiquidVisions.PanthaRhei.Generator.Application.Interactors.Generators;
using LiquidVisions.PanthaRhei.Generator.Application.Interactors.Initializers;
using LiquidVisions.PanthaRhei.Generator.Application.Interactors.Seeders;
using LiquidVisions.PanthaRhei.Generator.Application.Interactors.Templates;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Gateways;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Templates;
using Microsoft.Extensions.DependencyInjection;
//gerco
namespace LiquidVisions.PanthaRhei.Generator.Application 
{
    /// <summary>
    /// DependencyInjection extensions for the application library.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class DependencyInjectionExtension
    {
        /// <summary>
        /// Adds the dependencies of the project to the dependency inversion object.
        /// </summary>
        /// <param name="services"><seealso cref="IServiceCollection"/></param>
        /// <returns>An instance of <seealso cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            return services.AddTransient<ICodeGeneratorBuilderInteractor, CodeGeneratorBuilderInteractor>()
                .AddTransient<IEntitiesToSeedGateway, EntitiesToSeedGateway>()
                .AddTransient<ICodeGeneratorInteractor, CodeGeneratorInteractor>()
                .AddInitializers()
                .AddSeedersInteractors()
                .AddBoundaries()
                .AddTemplateInteractors();
        }

        private static IServiceCollection AddTemplateInteractors(this IServiceCollection services)
        {
            services.AddTransient<ITemplateInteractor, ScribanTemplateInteractor>()
                .AddTransient<ITemplateLoaderInteractor, TemplateLoaderInteractor>();

            return services;
        }

        private static IServiceCollection AddInitializers(this IServiceCollection services)
        {
            return services.AddTransient<IExpanderPluginLoaderInteractor, ExpanderPluginLoaderInteractor>()
                .AddTransient<IAssemblyContextInteractor, AssemblyContextInteractor>()
                .AddTransient<IAssemblyContextInteractor, AssemblyContextInteractor>()
                .AddTransient<IExpanderPluginLoaderInteractor, ExpanderPluginLoaderInteractor>()
                .AddTransient<IObjectActivatorInteractor, ObjectActivatorInteractor>();
        }

        private static IServiceCollection AddBoundaries(this IServiceCollection services)
        {
            return services.AddTransient<ICodeGeneratorBoundary, CodeGeneratorServiceBoundary>()
                .AddTransient<ISeedingBoundary, SeedingBoundary>();
        }

        private static IServiceCollection AddSeedersInteractors(this IServiceCollection services)
        {
            services.AddTransient<ISeederInteractor<App>, AppSeederInteractor>()
                .AddTransient<ISeederInteractor<App>, ExpanderSeederInteractor>()
                .AddTransient<ISeederInteractor<App>, EntitySeederInteractor>()
                //.AddTransient<ISeederInteractor<App>, PackageSeederInteractor>()
                .AddTransient<ISeederInteractor<App>, FieldSeederInteractor>()
                .AddTransient<ISeederInteractor<App>, ComponentSeederInteractor>()
                .AddTransient<ISeederInteractor<App>, ConnectionStringsSeederInteractor>()
                .AddTransient<ISeederInteractor<App>, RelationshipSeederInteractor>();

            return services;
        }
    }
}
