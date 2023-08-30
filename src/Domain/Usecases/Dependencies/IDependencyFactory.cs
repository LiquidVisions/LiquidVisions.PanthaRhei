using System.Collections.Generic;

namespace LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies
{
    /// <summary>
    /// Specifies an interface for an object that is able to resolve inversed dependencies.
    /// </summary>
    public interface IDependencyFactory
    {
        /// <summary>
        /// Gets a collection of the services.
        /// </summary>
        /// <typeparam name="TService">The type of the service to get.</typeparam>
        /// <returns><seealso cref="IEnumerable{T}"/></returns>
        IEnumerable<TService> ResolveAll<TService>();

        /// <summary>
        /// Gets the services from the dependecy container.
        /// </summary>
        /// <typeparam name="T">The service type to resolve.</typeparam>
        /// <returns><typeparamref name="T"/>.</returns>
        T Resolve<T>();
    }
}
