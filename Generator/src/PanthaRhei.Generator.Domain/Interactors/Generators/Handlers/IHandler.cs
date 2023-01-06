using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Expanders;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Handlers
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

        /// <summary>
        /// Gets the order in whitch the handler should be executed.
        /// </summary>
        int Order { get; }

        /// <summary>
        /// Gets the Expander that is of type <typeparamref name="TExpander"/>.
        /// </summary>
        TExpander Expander { get; }
    }
}
