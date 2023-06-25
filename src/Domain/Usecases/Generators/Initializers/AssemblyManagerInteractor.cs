using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Initializers
{
    /// <summary>
    /// Implements an assembly manager.
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal class AssemblyManagerInteractor : IAssemblyManagerInteractor
    {
        /// <inheritdoc/>
        public Assembly GetAssembly(Type type)
        {
            return type.Assembly;
        }
    }
}
