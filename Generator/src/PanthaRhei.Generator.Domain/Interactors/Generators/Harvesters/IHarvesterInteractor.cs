using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Expanders;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Harvesters
{
    /// <summary>
    /// Specifies an interface that executes Harvesters.
    /// </summary>
    /// <typeparam name="TExpander"><seealso cref="IExpanderInteractor"/></typeparam>
    public interface IHarvesterInteractor<out TExpander> : IExecutionInteractor
        where TExpander : class, IExpanderInteractor
    {
        /// <summary>
        /// Gets the <seealso cref="Expander"/>.
        /// </summary>
        public TExpander Expander { get; }
    }
}
