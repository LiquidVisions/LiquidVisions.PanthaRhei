using Microsoft.Extensions.DependencyInjection;

namespace LiquidVisions.PanthaRhei.Generator.Presentation.Cli
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
        /// <returns>An instance of <seealso cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddConsole(this IServiceCollection services)
        {
            return services;
        }
    }
}
