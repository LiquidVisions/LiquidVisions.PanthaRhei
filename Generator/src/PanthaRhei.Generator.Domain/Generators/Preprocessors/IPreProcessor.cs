﻿using LiquidVisions.PanthaRhei.Generator.Domain.Generators.Expanders;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Generators.Preprocessors
{
    /// <summary>
    /// Represents a handler that executes post processing actions.
    /// </summary>
    /// <typeparam name="TExpander"><seealso cref="IExpander"/></typeparam>
    public interface IPreProcessor<out TExpander> : IProcessor<TExpander>
        where TExpander : class, IExpander
    {
    }
}
