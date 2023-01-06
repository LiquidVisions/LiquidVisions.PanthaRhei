using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.Loader;

namespace LiquidVisions.PanthaRhei.Generator.Application.Interactors.Initializers
{
    /// <summary>
    /// Loads the Expander plugins from a given location.
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal class AssemblyContextInteractor : AssemblyLoadContext, IAssemblyContextInteractor
    {
        private AssemblyDependencyResolver resolver;

        /// <inheritdoc/>
        public Assembly Load(string assemblyName)
        {
            resolver = new AssemblyDependencyResolver(assemblyName);
            Assembly assembly = LoadFromAssemblyName(new AssemblyName(System.IO.Path.GetFileNameWithoutExtension(assemblyName)));

            return assembly;
        }

        /// <inheritdoc/>
        protected override Assembly Load(AssemblyName assemblyName)
        {
            string assemblyPath = resolver.ResolveAssemblyToPath(assemblyName);
            if (assemblyPath != null)
            {
                return LoadFromAssemblyPath(assemblyPath);
            }

            return null;
        }

        /// <inheritdoc/>
        protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
        {
            string libraryPath = resolver.ResolveUnmanagedDllToPath(unmanagedDllName);
            if (libraryPath != null)
            {
                return LoadUnmanagedDllFromPath(libraryPath);
            }

            return IntPtr.Zero;
        }
    }
}
