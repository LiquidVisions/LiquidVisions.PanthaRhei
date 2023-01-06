using LiquidVisions.PanthaRhei.Generator.Domain.GeneratorUseCases.Expanders;
using LiquidVisions.PanthaRhei.Generator.Domain.Models;

namespace LiquidVisions.PanthaRhei.Generator.Domain.GeneratorUseCases.Handlers
{
    /// <summary>
    /// Specifies the interface of an expander handler.
    /// </summary>
    /// <typeparam name="TExpander"><seealso cref="IExpander"/></typeparam>
    public interface IHandler<out TExpander> : IExecutionManager
        where TExpander : class, IExpander
    {
        /// <summary>
        /// Gets the name of the <see cref="IHandler{TExpander}"/>.
        /// </summary>
        string Name { get; }

        Handler Model { get; }

        /// <summary>
        /// Gets the Expander that is of type <typeparamref name="TExpander"/>.
        /// </summary>
        TExpander Expander { get; }
    }
}
