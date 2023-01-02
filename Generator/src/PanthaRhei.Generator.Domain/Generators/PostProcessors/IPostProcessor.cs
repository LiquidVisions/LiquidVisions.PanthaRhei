using LiquidVisions.PanthaRhei.Generator.Domain.Generators.Expanders;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Generators.PostProcessors
{
    /// <summary>
    /// Represents a handler that executes post processing actions.
    /// </summary>
    /// <typeparam name="TExpander"><seealso cref="IExpander"/></typeparam>
    public interface IPostProcessor<out TExpander> : IProcessor<TExpander>
        where TExpander : class, IExpander
    {
    }
}
