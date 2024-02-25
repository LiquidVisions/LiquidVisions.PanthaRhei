using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies
{
    /// <summary>
    /// The <see cref="IServiceCollection">dependency container</see>.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="DependencyManager"/> class.
    /// </remarks>
    /// <param name="wrapper">The <see cref="IServiceCollectionWrapper"/>.</param>
    internal class DependencyManager(IServiceCollectionWrapper wrapper) : IDependencyFactory, IDependencyManager
    {
        private IServiceProvider provider;

        /// <inheritdoc/>
        public void AddTransient(Type serviceType, Type implementationType)
        {
            wrapper.AddTransient(serviceType, implementationType);
        }

        /// <inheritdoc/>
        public IDependencyFactory Build()
        {
            provider = wrapper.BuildServiceProvider();

            return this;
        }

        /// <inheritdoc/>
        public IEnumerable<T> ResolveAll<T>()
        {
            if (provider == null)
            {
                Build();
            }

            return provider.GetServices<T>();
        }

        /// <inheritdoc/>
        public T Resolve<T>()
        {
            if (provider == null)
            {
                Build();
            }

            return provider.GetRequiredService<T>();
        }

        /// <inheritdoc/>
        public void AddSingleton<T>(T singletonObject)
            where T : class => wrapper.AddSingleton(singletonObject);

        /// <inheritdoc/>
        public void AddSingleton(Type serviceType, Type implementationType)
        {
            wrapper.AddSingleton(serviceType, implementationType);
        }
    }
}
