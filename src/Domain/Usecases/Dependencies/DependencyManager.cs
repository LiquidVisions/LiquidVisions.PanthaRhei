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
    /// <param name="serviceCollection">The <see cref="IServiceCollection"/>.</param>
    internal class DependencyManager(IServiceCollection serviceCollection) : IDependencyFactory, IDependencyManager
    {
        private IServiceProvider _provider;

        /// <inheritdoc/>
        public void AddTransient(Type serviceType, Type implementationType)
        {
            serviceCollection.AddTransient(serviceType, implementationType);
        }

        /// <inheritdoc/>
        public IDependencyFactory Build()
        {
            _provider = serviceCollection.BuildServiceProvider();

            return this;
        }

        /// <inheritdoc/>
        public IEnumerable<T> ResolveAll<T>()
        {
            if (_provider == null)
            {
                Build();
            }

            return _provider.GetServices<T>();
        }

        /// <inheritdoc/>
        public T Resolve<T>()
        {
            if (_provider == null)
            {
                Build();
            }

            return _provider.GetRequiredService<T>();
        }

        /// <inheritdoc/>
        public void AddSingleton<T>(T singletonObject)
            where T : class => serviceCollection.AddSingleton(singletonObject);

        /// <inheritdoc/>
        public void AddSingleton(Type serviceType, Type implementationType)
        {
            serviceCollection.AddSingleton(serviceType, implementationType);
        }
    }
}
