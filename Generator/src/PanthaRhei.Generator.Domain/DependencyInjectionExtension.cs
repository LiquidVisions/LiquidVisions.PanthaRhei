using System.Runtime.Loader;
using System;
using LiquidVisions.PanthaRhei.Generator.Domain.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Initializers;
using LiquidVisions.PanthaRhei.Generator.Domain.Models;
using LiquidVisions.PanthaRhei.Generator.Domain.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace LiquidVisions.PanthaRhei.Generator.Domain
{
    /// <summary>
    /// DependencyInjection extensions for the domain library.
    /// </summary>
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
                .AddTransient<IExpanderPluginLoader, ExpanderPluginLoader>()
                .AddInitializers();

            return services;
        }

        private static IServiceCollection AddInitializers(this IServiceCollection services)
        {
            return services.AddTransient<IAssemblyContext, ExpanderPluginLoadContext>()
                .AddTransient<IAssemblyManager, AssemblyManager>()
                .AddTransient<IAssemblyContext, ExpanderPluginLoadContext>()
                .AddTransient<IExpanderPluginLoader, ExpanderPluginLoader>()
                .AddTransient<IObjectActivator, ObjectActivator>();
        }
    }
}
