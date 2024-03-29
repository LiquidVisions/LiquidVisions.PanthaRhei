﻿using System;

namespace LiquidVisions.PanthaRhei.Application.Usecases.Initializers
{
    /// <summary>
    /// Represents an interface that is able to activate objects using Reflection.
    /// </summary>
    internal interface IObjectActivator
    {
        /// <summary>
        /// Creates an instance of a type.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> that needs to be instanciated.</param>
        /// <param name="objects">the constructor options.</param>
        /// <returns>The instanciated object.</returns>
        object CreateInstance(Type type, params object[] objects);
    }
}
