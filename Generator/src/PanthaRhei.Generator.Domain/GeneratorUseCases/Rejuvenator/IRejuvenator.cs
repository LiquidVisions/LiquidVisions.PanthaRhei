using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.GeneratorUseCases.Expanders;

namespace LiquidVisions.PanthaRhei.Generator.Domain.GeneratorUseCases.Rejuvenator
{
    /// <summary>
    /// Represents an interface that executes rejuvenators.
    /// </summary>
    /// <typeparam name="TExpander"><seealso cref="IExpander"/></typeparam>
    public interface IRejuvenator<out TExpander> : IExecutionManager
        where TExpander : class, IExpander
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
