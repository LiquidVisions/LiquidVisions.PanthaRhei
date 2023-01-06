using System.Diagnostics.CodeAnalysis;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Initializers;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Seeders;
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
            var container = new DependencyInjectionContainer(services);

            services.AddSingleton<IDependencyManager>(container)
                .AddSingleton<IDependencyResolver>(container)
                .AddSingleton(new Parameters())
                .AddTransient<ICodeGeneratorBuilder, CodeGeneratorBuilder>()
                .AddTransient<ICodeGenerator, CodeGenerator>()
                .AddTransient<IExpanderPluginLoader, ExpanderPluginLoader>()
                .AddTransient<IPluralizer, CustomPluralizer>()
                .AddSingleton<IProjectAgentInteractor, ProjectAgent>()
                .AddInitializers()
                .AddTemplateServices()
                .AddModelInitializers();

            return services;
        }

        private static IServiceCollection AddModelInitializers(this IServiceCollection services)
        {
            services.AddTransient<ISeeder<App>, AppSeeder>()
                .AddTransient<ISeeder<App>, ExpanderSeeder>()
                .AddTransient<ISeeder<App>, EntitySeeder>()
                .AddTransient<ISeeder<App>, PackageSeeder>()
                .AddTransient<ISeeder<App>, FieldSeeder>()
                .AddTransient<ISeeder<App>, ComponentSeeder>();

            return services;
        }

        private static IServiceCollection AddTemplateServices(this IServiceCollection services)
        {
            services.AddTransient<ITemplateService, ScribanTemplateService>()
                .AddTransient<ITemplateLoader, TemplateLoader>();

            return services;
        }

        private static IServiceCollection AddInitializers(this IServiceCollection services)
        {
            return services.AddTransient<IAssemblyContext, ExpanderPluginLoadContext>()
                .AddTransient<IAssemblyManager, AssemblyManager>()
                .AddTransient<IAssemblyContext, ExpanderPluginLoadContext>()
                .AddTransient<IExpanderPluginLoader, ExpanderPluginLoader>()
                .AddTransient<ISerializer<Harvest>, CustomSerializer<Harvest>>()
                .AddTransient<IDeserializer<Harvest>, CustomSerializer<Harvest>>()
                .AddTransient<IObjectActivator, ObjectActivator>();
        }
    }
}
