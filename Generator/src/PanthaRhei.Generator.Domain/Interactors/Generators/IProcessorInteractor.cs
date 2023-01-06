using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Expanders;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators
{
    /// <summary>
    /// Represents a handler that executes processing actions.
    /// </summary>
    public interface IProcessorInteractor : IExecutionInteractor
    {
    }

    /// <summary>
    /// Represents a handler that executes post processing actions.
    /// </summary>
    /// <typeparam name="TExpander"><seealso cref="IExpanderInteractor"/></typeparam>
    public interface IProcessorInteractor<out TExpander> : IProcessorInteractor
        where TExpander : class, IExpanderInteractor
    {
        /// <summary>
        /// Gets the <seealso cref="App"/>.
        /// </summary>
        App App { get; }

        /// <summary>
        /// Gets the <seealso cref="Expander"/>.
        /// </summary>
        public TExpander Expander { get; }
    }
}
