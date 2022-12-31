using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Infrastructure;
using LiquidVisions.PanthaRhei.Generator.Infrastructure.EntityFramework;
using Microsoft.Extensions.DependencyInjection;

namespace LiquidVisions.PanthaRhei.Generator.Application
{
    /// <summary>
    /// DependencyInjection extensions for the application library.
    /// </summary>
    public static class DependencyInjectionExtension
    {
        /// <summary>
        /// Adds the dependencies of the project to the dependency inversion object.
        /// </summary>
        /// <param name="services"><seealso cref="IServiceCollection"/></param>
        /// <returns>An instance of <seealso cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddTransient<ICodeGeneratorService, CodeGeneratorService>();

            return services.AddDomainLayer()
                .AddInfrastructureLayer()
                .AddEntityFrameworkLayer();
        }
    }
}
