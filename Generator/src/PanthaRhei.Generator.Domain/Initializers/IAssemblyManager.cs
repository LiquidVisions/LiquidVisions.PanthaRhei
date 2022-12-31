using System;
using System.Reflection;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Initializers
{
    /// <summary>
    /// Specifies an assembly manager.
    /// </summary>
    internal interface IAssemblyManager
    {
        /// <summary>
        /// Gets the <seealso cref="Assembly"/> of the given <seealso cref="Type"/>.
        /// </summary>
        /// <param name="type"><see cref="Type"/>.</param>
        /// <returns><seealso cref="Assembly"/>.</returns>
        Assembly GetAssembly(Type type);
    }
}
