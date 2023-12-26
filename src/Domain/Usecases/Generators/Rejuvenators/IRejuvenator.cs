using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Expanders;

namespace LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Rejuvenators
{
    /// <summary>
    /// Represents an interface that executes rejuvenators.
    /// </summary>
    /// <typeparam name="TExpander"><seealso cref="IExpander"/></typeparam>
    public interface IRejuvenator<out TExpander> : ICommand
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
