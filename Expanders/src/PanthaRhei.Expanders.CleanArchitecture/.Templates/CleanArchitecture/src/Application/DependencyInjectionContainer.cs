using System;
using System.Collections.Generic;
using NS.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace NS.Application
{
    /// <summary>
    /// The <see cref="IServiceCollection">dependency container</see>.
    /// </summary>
    public class DependencyInjectionContainer : IDependencyServiceProvider
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
        private IDependencyServiceProvider Build()
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
