using LiquidVisions.PanthaRhei.Application;
using LiquidVisions.PanthaRhei.Application.RequestModels;
using LiquidVisions.PanthaRhei.Infrastructure;
using LiquidVisions.PanthaRhei.Infrastructure.EntityFramework;
using Microsoft.Extensions.DependencyInjection;

namespace LiquidVisions.PanthaRhei.Presentation.Cli
{
    /// <summary>
    /// DependencyInjection extensions for the infrastructure.console library.
    /// </summary>
    public static class DependencyInjectionExtension
    {
        /// <summary>
        /// Adds the dependencies of the project to the dependency inversion object.
        /// </summary>
        /// <param name="services"><seealso cref="IServiceCollection"/></param>
        /// <param name="model"><seealso cref="ExpandOptionsRequestModel"/></param>
        /// <returns>An instance of <seealso cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddPresentationLayer(this IServiceCollection services, ExpandOptionsRequestModel model)
        {
            services.AddApplicationLayer(model)
                .AddInfrastructureLayer()
                .AddEntityFrameworkLayer();

            return services;
        }
    }
}
