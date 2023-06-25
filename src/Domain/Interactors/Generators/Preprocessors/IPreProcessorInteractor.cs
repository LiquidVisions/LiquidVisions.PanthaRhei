﻿using LiquidVisions.PanthaRhei.Domain.Interactors.Generators.Expanders;

namespace LiquidVisions.PanthaRhei.Domain.Interactors.Generators.Preprocessors
{
    /// <summary>
    /// Represents a handler that executes post processing actions.
    /// </summary>
    /// <typeparam name="TExpander"><seealso cref="IExpander"/></typeparam>
    public interface IPreProcessorInteractor<out TExpander> : IProcessorInteractor<TExpander>
        where TExpander : class, IExpander
    {
    }
}
