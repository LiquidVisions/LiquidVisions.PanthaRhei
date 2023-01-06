using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Initializers
{
    /// <summary>
    /// Implements an assembly manager.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class AssemblyManager : IAssemblyManager
    {
        /// <inheritdoc/>
        public Assembly GetAssembly(Type type)
        {
            return type.Assembly;
        }
    }
}
