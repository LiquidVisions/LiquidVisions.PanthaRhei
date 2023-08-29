using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies
{
    /// <summary>
    /// The <see cref="IServiceCollection">dependency container</see>.
    /// </summary>
    internal class DependencyManager : IDependencyFactory, IDependencyManager
    {
        private readonly IServiceCollection _serviceCollection;
        private IServiceProvider _provider;

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyManager"/> class.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection"/>.</param>
        public DependencyManager(IServiceCollection serviceCollection)
        {
            _serviceCollection = serviceCollection;
        }

        /// <inheritdoc/>
        public void AddTransient(Type serviceType, Type implementationType)
        {
            _serviceCollection.AddTransient(serviceType, implementationType);
        }

        /// <inheritdoc/>
        public IDependencyFactory Build()
        {
            _provider = _serviceCollection.BuildServiceProvider();

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
            where T : class => _serviceCollection.AddSingleton(singletonObject);

        /// <inheritdoc/>
        public void AddSingleton(Type serviceType, Type implementationType)
        {
            _serviceCollection.AddSingleton(serviceType, implementationType);
        }
    }
}
