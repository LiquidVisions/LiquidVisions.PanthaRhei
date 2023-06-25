using LiquidVisions.PanthaRhei.Domain.Interactors.Generators.Expanders;

namespace LiquidVisions.PanthaRhei.Domain.Interactors.Generators.PostProcessors
{
    /// <summary>
    /// Represents a handler that executes post processing actions.
    /// </summary>
    /// <typeparam name="TExpander"><seealso cref="IExpanderInteractor"/></typeparam>
    public interface IPostProcessorInteractor<out TExpander> : IProcessorInteractor<TExpander>
        where TExpander : class, IExpanderInteractor
    {
    }
}
