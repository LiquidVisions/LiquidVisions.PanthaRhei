using LiquidVisions.PanthaRhei.Generator.Domain.Generators.Expanders;
using LiquidVisions.PanthaRhei.Generator.Domain.Models;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Generators.Rejuvenator
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
