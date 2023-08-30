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
#pragma warning disable S3885 // "Assembly.Load" should be used
            Assembly loadedAssembly = Assembly.LoadFrom(assemblyFilePath);
#pragma warning restore S3885 // "Assembly.Load" should be used

            return loadedAssembly;
        }
    }
}
