using System.Reflection;

namespace LiquidVisions.PanthaRhei.Application.Usecases.Initializers
{
    /// <summary>
    /// Specifies an interface for an Assembly loader context.
    /// </summary>
    internal interface IAssemblyContext
    {
        /// <summary>
        /// Loads and returns an <see cref="Assembly"/>.
        /// </summary>
        /// <param name="assemblyFilePath">The name of the assembly.</param>
        /// <returns><see cref="Assembly"/>.</returns>
        Assembly Load(string assemblyFilePath);
        Assembly Load(AssemblyName assemblyName);
    }
}
