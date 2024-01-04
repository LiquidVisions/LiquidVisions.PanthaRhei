using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies
{
    /// <summary>
    /// Wrapper for the <see cref="IServiceCollection"/>.
    /// This class is needed in order to be able to test the <see cref="DependencyManager"/> class.
    /// </summary>
    internal interface IServiceCollectionWrapper
    {
        /// <summary>
        /// Adds a singleton service of the type specified in serviceType with an implementation.
        /// </summary>
        /// <param name="singletonObject">The Singleton type</param>
        void AddSingleton<T>(T singletonObject) where T : class;

        /// <summary>
        /// Adds a singleton service of the type specified in serviceType with an implementation.
        /// </summary>
        /// <param name="serviceType">The service type</param>
        /// <param name="implementationType">The Implementation type</param>
        void AddSingleton(Type serviceType, Type implementationType);

        /// <summary>
        /// Adds a transient service of the type specified in serviceType with an implementation.
        /// </summary>
        /// <param name="serviceType">The service type</param>
        /// <param name="implementationType">The implementation type</param>
        void AddTransient(Type serviceType, Type implementationType);

        /// <summary>
        /// Builds the service provider.
        /// </summary>
        /// <returns><seealso cref="IServiceProvider"/></returns>
        IServiceProvider BuildServiceProvider();
    }

    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    internal class ServiceCollectionWrapper(IServiceCollection collection) : IServiceCollectionWrapper
    {
        /// <inheritdoc/>
        public virtual void AddTransient(Type serviceType, Type implementationType)
            => collection.AddTransient(serviceType, implementationType);

        /// <inheritdoc/>
        public virtual void AddSingleton<T>(T singletonObject) where T : class
            => collection.AddSingleton(singletonObject);

        /// <inheritdoc/>
        public virtual void AddSingleton(Type serviceType, Type implementationType)
            => collection.AddSingleton(serviceType, implementationType);

        /// <inheritdoc/>
        public virtual IServiceProvider BuildServiceProvider()
            => collection.BuildServiceProvider();
    }   
}
