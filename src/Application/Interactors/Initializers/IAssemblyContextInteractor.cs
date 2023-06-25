using System.Reflection;

namespace LiquidVisions.PanthaRhei.Application.Interactors.Initializers
{
    /// <summary>
    /// Specifies an interface for an Assembly loader context.
    /// </summary>
    internal interface IAssemblyContextInteractor
    {
        /// <summary>
        /// Loads and returns an <see cref="Assembly"/>.
        /// </summary>
        /// <param name="assemblyName">The name of the assembly.</param>
        /// <returns><see cref="Assembly"/>.</returns>
        Assembly Load(string assemblyName);
    }
}
