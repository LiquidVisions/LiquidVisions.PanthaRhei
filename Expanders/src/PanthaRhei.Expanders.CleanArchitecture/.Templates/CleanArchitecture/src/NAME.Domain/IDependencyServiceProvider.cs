using System.Collections.Generic;

namespace NS.Domain
{
    /// <summary>
    /// Specifies an interface for an agnostic Dependency provider.
    /// </summary>
    public interface IDependencyServiceProvider
    {
        /// <summary>
        /// Gets a collection of the services.
        /// </summary>
        /// <typeparam name="TService">The type of the service to get.</typeparam>
        /// <returns><seealso cref="IEnumerable{T}"/></returns>
        IEnumerable<TService> GetAll<TService>();

        /// <summary>
        /// Gets the services from the dependecy container.
        /// </summary>
        /// <typeparam name="T">The service type to resolve.</typeparam>
        /// <returns><typeparamref name="T"/>.</returns>
        T Get<T>();
    }
}
