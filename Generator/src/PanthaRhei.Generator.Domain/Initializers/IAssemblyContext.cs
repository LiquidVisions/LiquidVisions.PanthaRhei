using System.Reflection;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Initializers
{
    /// <summary>
    /// Specifies an interface for an Assembly loader context.
    /// </summary>
    internal interface IAssemblyContext
    {
        /// <summary>
        /// Loads and returns an <see cref="Assembly"/>.
        /// </summary>
        /// <param name="assemblyName">The name of the assembly.</param>
        /// <returns><see cref="Assembly"/>.</returns>
        Assembly Load(string assemblyName);
    }
}
