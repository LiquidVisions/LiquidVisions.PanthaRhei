using System;
using LiquidVisions.PanthaRhei.Generator.Domain.Models;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Gateways
{
    /// <summary>
    /// Represents a contract for an object that is able to load the model of type <seealso cref="App"/>.
    /// </summary>
    public interface IAppRepository
    {
        /// <summary>
        /// Gets an <seealso cref="App"/> by <see cref="Guid">Id</see>.
        /// </summary>
        /// <param name="appId">The id of the <seealso cref="App"/> that will be loaded.</param>
        /// <returns>a full instance of <seealso cref="App"/>.</returns>
        App GetById(Guid appId);
    }
}
