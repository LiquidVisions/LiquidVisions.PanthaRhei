﻿using LiquidVisions.PanthaRhei.Domain.Interactors.Generators.Expanders;

namespace LiquidVisions.PanthaRhei.Domain.Interactors.Generators.Harvesters
{
    /// <summary>
    /// Specifies an interface that executes Harvesters.
    /// </summary>
    /// <typeparam name="TExpander"><seealso cref="IExpanderInteractor"/></typeparam>
    public interface IHarvesterInteractor<out TExpander> : ICommand
        where TExpander : class, IExpanderInteractor
    {
        /// <summary>
        /// Gets the <seealso cref="Expander"/>.
        /// </summary>
        public TExpander Expander { get; }
    }
}
