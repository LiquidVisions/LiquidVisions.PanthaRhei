using System;
using System.Diagnostics.CodeAnalysis;

namespace LiquidVisions.PanthaRhei.Application.Interactors.Initializers
{
    /// <summary>
    /// A class that is able to activate objects using reflection.
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal class ObjectActivatorInteractor : IObjectActivatorInteractor
    {
        /// <inheritdoc/>
        public object CreateInstance(Type type, params object[] objects)
        {
            return Activator.CreateInstance(type, objects);
        }
    }
}
