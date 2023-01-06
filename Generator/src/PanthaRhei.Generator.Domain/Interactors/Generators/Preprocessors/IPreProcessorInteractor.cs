using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Expanders;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Preprocessors
{
    /// <summary>
    /// Represents a handler that executes post processing actions.
    /// </summary>
    /// <typeparam name="TExpander"><seealso cref="IExpanderInteractor"/></typeparam>
    public interface IPreProcessorInteractor<out TExpander> : IProcessorInteractor<TExpander>
        where TExpander : class, IExpanderInteractor
    {
    }
}
