using System;

namespace LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies
{
    /// <summary>
    /// Specifies an interface for an agnostic Dependency registrations.
    /// </summary>
    public interface IDependencyManager
    {
        /// <summary>
        /// Adds a singleton service of the type specified in TService with an instance specified
        ///  in implementationInstance to the specified Microsoft.Extensions.DependencyInjection.IServiceCollection.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <param name="singletonObject">The instance of the service to add.</param>
        void AddSingleton<TService>(TService singletonObject)
            where TService : class;

        /// <summary>
        /// Adds a singleton service of the type specified in serviceType with an implementation.
        /// </summary>
        /// <param name="serviceType">The service type.</param>
        /// <param name="implementationType">The implementation type.</param>
        void AddSingleton(Type serviceType, Type implementationType);

        /// <summary>
        /// Adds a scoped service of the type specified in serviceType with an implementation.
        /// </summary>
        /// <param name="serviceType">The service type.</param>
        /// <param name="implementationType">The implementation type.</param>
        void AddTransient(Type serviceType, Type implementationType);

        /// <summary>
        /// Builds the Dependency container.
        /// </summary>
        /// <returns><seealso cref="IDependencyFactory"/></returns>
        IDependencyFactory Build();
    }
}
