﻿using System.Diagnostics.CodeAnalysis;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Initializers;
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
        /// <param name="options"><seealso cref="GenerationOptions"/></param>
        /// <returns>An instance of <seealso cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddDomainLayer(this IServiceCollection services, GenerationOptions options)
        {
            var container = new DependencyManagerInteractor(services);

            return services.AddSingleton<IDependencyManagerInteractor>(container)
                .AddSingleton<IDependencyFactoryInteractor>(container)
                .AddSingleton(options)
                .AddInitializers();
        }

        private static IServiceCollection AddInitializers(this IServiceCollection services)
        {
            return services
                .AddSingleton<IAssemblyManagerInteractor, AssemblyManagerInteractor>();
        }
    }
}
