using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace LiquidVisions.PanthaRhei.Application.Usecases.Initializers
{
    /// <summary>
    /// Loads the Expander plugins from a given location.
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal class AssemblyContext : IAssemblyContext
    {
        /// <inheritdoc/>
        public Assembly Load(string assemblyFilePath)
        {
            Assembly loadedAssembly = Assembly.LoadFrom(assemblyFilePath);

            return loadedAssembly;
        }
    }
}
