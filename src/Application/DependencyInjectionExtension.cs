using System;
using System.Diagnostics.CodeAnalysis;
using LiquidVisions.PanthaRhei.Application.Boundaries;
using LiquidVisions.PanthaRhei.Application.RequestModels;
using LiquidVisions.PanthaRhei.Application.Usecases.Generators;
using LiquidVisions.PanthaRhei.Application.Usecases.Initializers;
using LiquidVisions.PanthaRhei.Application.Usecases.Seeders;
using LiquidVisions.PanthaRhei.Application.Usecases.Templates;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Repositories;
using LiquidVisions.PanthaRhei.Domain.Usecases.Templates;
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
                .AddTransient<ICodeGeneratorBuilder, CodeGeneratorBuilder>()
                .AddTransient<IEntitiesToSeedRepository, EntitiesToSeedGateway>()
                .AddTransient<ICodeGenerator, CodeGenerator>()
                .AddInitializers()
                .AddSeedersInteractors()
                .AddBoundaries()
                .AddTemplateInteractors();
        }

        private static IServiceCollection AddTemplateInteractors(this IServiceCollection services)
        {
            services.AddTransient<ITemplateInteractor, ScribanTemplate>()
                .AddTransient<ITemplateLoaderInteractor, TemplateLoader>()
                .AddTransient<ScriptObject, CustomScripts>();

            return services;
        }

        private static IServiceCollection AddInitializers(this IServiceCollection services)
        {
            return services.AddTransient<IExpanderPluginLoader, ExpanderPluginLoader>()
                .AddTransient<IAssemblyContext, AssemblyContext>()
                .AddTransient<IAssemblyContext, AssemblyContext>()
                .AddTransient<IExpanderPluginLoader, ExpanderPluginLoader>()
                .AddTransient<IObjectActivator, ObjectActivator>();
        }

        private static IServiceCollection AddBoundaries(this IServiceCollection services)
        {
            return services.AddTransient<IExpandBoundary, ExpandBoundary>()
                .AddTransient<ISeeder, Seeder>();
        }

        private static IServiceCollection AddSeedersInteractors(this IServiceCollection services)
        {
            services.AddTransient<IEntitySeeder<App>, AppSeeder>()
                .AddTransient<IEntitySeeder<App>, ExpanderSeeder>()
                .AddTransient<IEntitySeeder<App>, EntitySeeder>()
                .AddTransient<IEntitySeeder<App>, FieldSeeder>()
                .AddTransient<IEntitySeeder<App>, ComponentSeeder>()
                .AddTransient<IEntitySeeder<App>, ConnectionStringsSeeder>()
                .AddTransient<IEntitySeeder<App>, RelationshipSeeder>();

            return services;
        }
    }
}
