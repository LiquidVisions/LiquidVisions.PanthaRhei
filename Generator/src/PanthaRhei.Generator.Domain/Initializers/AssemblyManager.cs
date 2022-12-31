using System;
using System.Reflection;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Initializers
{
    /// <summary>
    /// Implements an assembly manager.
    /// </summary>
    public class AssemblyManager : IAssemblyManager
    {
        /// <inheritdoc/>
        public Assembly GetAssembly(Type type)
        {
            return type.Assembly;
        }
    }
}
