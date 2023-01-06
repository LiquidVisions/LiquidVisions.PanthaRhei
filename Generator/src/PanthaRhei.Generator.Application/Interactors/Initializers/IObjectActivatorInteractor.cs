using System;

namespace LiquidVisions.PanthaRhei.Generator.Application.Interactors.Initializers
{
    /// <summary>
    /// Represents an interface that is able to activate objects using Reflection.
    /// </summary>
    internal interface IObjectActivatorInteractor
    {
        /// <summary>
        /// Creates an instance of a type.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> that needs to be instanciated.</param>
        /// <param name="objects">the constructor parameters.</param>
        /// <returns>The instanciated object.</returns>
        object CreateInstance(Type type, params object[] objects);
    }
}
