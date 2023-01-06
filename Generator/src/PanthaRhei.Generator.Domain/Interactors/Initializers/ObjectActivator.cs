using System;
using System.Diagnostics.CodeAnalysis;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Initializers
{
    /// <summary>
    /// A class that is able to activate objects using reflection.
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal class ObjectActivator : IObjectActivator
    {
        /// <inheritdoc/>
        public object CreateInstance(Type type, params object[] objects)
        {
            return Activator.CreateInstance(type, objects);
        }
    }
}
