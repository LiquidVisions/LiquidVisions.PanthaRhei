using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Expanders;

namespace LiquidVisions.PanthaRhei.Domain.Usecases.Generators
{
    /// <summary>
    /// Specifies the interface of an expander handler.
    /// </summary>
    /// <typeparam name="TExpander"><seealso cref="IExpander"/></typeparam>
    public interface IExpanderTask<out TExpander> : ICommand
        where TExpander : class, IExpander
    {
        /// <summary>
        /// Gets the name of the <see cref="IExpanderTask{TExpander}"/>.
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
