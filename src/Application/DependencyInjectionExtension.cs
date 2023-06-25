using System;
using System.Diagnostics.CodeAnalysis;
using LiquidVisions.PanthaRhei.Application.Boundaries;
using LiquidVisions.PanthaRhei.Application.Interactors.Gateways;
using LiquidVisions.PanthaRhei.Application.Interactors.Generators;
using LiquidVisions.PanthaRhei.Application.Interactors.Initializers;
using LiquidVisions.PanthaRhei.Application.Interactors.Seeders;
using LiquidVisions.PanthaRhei.Application.Interactors.Templates;
using LiquidVisions.PanthaRhei.Application.RequestModels;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Gateways;
using LiquidVisions.PanthaRhei.Domain.Interactors.Templates;
using Microsoft.Extensions.DependencyInjection;
using Scriban.Runtime;

namespace LiquidVisions.PanthaRhei.Application
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
        /// <param name="requestModel"><seealso cref="ExpandOptionsRequestModel"/></param>
        /// <returns>An instance of <seealso cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services, ExpandOptionsRequestModel requestModel)
        {
            GenerationOptions options = new()
            {
                AppId = requestModel.AppId,
                Clean = requestModel.Clean,
                ConnectionString = requestModel.ConnectionString,
                ExpandersFolder = requestModel.ExpandersFolder,
                Modes = Enum.Parse<GenerationModes>(requestModel.GenerationMode),
                HarvestFolder = requestModel.HarvestFolder,
                OutputFolder = requestModel.OutputFolder,
                ReSeed = requestModel.ReSeed,
                Root = requestModel.Root,
            };

            return services.AddDomainLayer(options)
                .AddTransient<ICodeGeneratorBuilderInteractor, CodeGeneratorBuilderInteractor>()
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
                .AddTransient<ITemplateLoaderInteractor, TemplateLoaderInteractor>()
                .AddTransient<ScriptObject, CustomScripts>();

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
            return services.AddTransient<IExpandBoundary, ExpandBoundary>()
                .AddTransient<ISeederInteractor, SeederInteractor>();
        }

        private static IServiceCollection AddSeedersInteractors(this IServiceCollection services)
        {
            services.AddTransient<IEntitySeederInteractor<App>, AppSeederInteractor>()
                .AddTransient<IEntitySeederInteractor<App>, ExpanderSeederInteractor>()
                .AddTransient<IEntitySeederInteractor<App>, EntitySeederInteractor>()
                .AddTransient<IEntitySeederInteractor<App>, FieldSeederInteractor>()
                .AddTransient<IEntitySeederInteractor<App>, ComponentSeederInteractor>()
                .AddTransient<IEntitySeederInteractor<App>, ConnectionStringsSeederInteractor>()
                .AddTransient<IEntitySeederInteractor<App>, RelationshipSeederInteractor>();

            return services;
        }
    }
}
