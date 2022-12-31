using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace LiquidVisions.PanthaRhei.Generator.Domain
{
    /// <summary>
    /// The <see cref="IServiceCollection">dependency container</see>.
    /// </summary>
    internal class DependencyInjectionContainer : IDependencyResolver, IDependencyManager
    {
        private readonly IServiceCollection serviceCollection;
        private IServiceProvider provider;

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyInjectionContainer"/> class.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection"/>.</param>
        public DependencyInjectionContainer(IServiceCollection serviceCollection)
        {
            this.serviceCollection = serviceCollection;
        }

        /// <inheritdoc/>
        public void AddTransient(Type serviceType, Type implementationType)
        {
            serviceCollection.AddTransient(serviceType, implementationType);
        }

        /// <inheritdoc/>
        public void AddSingleton<T>(T singletonObject)
            where T : class
        {
            serviceCollection.AddSingleton<T>(singletonObject);
        }

        /// <inheritdoc/>
        public IDependencyResolver Build()
        {
            provider = serviceCollection.BuildServiceProvider();

            return this;
        }

        /// <inheritdoc/>
        public IEnumerable<T> GetAll<T>()
        {
            if (provider == null)
            {
                Build();
            }

            return provider.GetServices<T>();
        }

        /// <inheritdoc/>
        public T Get<T>()
        {
            if (provider == null)
            {
                Build();
            }

            return provider.GetRequiredService<T>();
        }
    }
}
