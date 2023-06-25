using LiquidVisions.PanthaRhei.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Interactors.Generators;
using LiquidVisions.PanthaRhei.Domain.Interactors.Generators.Expanders;

namespace LiquidVisions.PanthaRhei.Domain.Interactors.Generators.PostProcessors
{
    /// <summary>
    /// An abstract implementation of the <see cref="ProcessorInteractor{TExpander}"/>.
    /// </summary>
    /// <typeparam name="TExpander">A deriveded type of <see cref="IExpanderInteractor"/>.</typeparam>
    public abstract class PostProcessorInteractor<TExpander> : ProcessorInteractor<TExpander>, IPostProcessorInteractor<TExpander>
        where TExpander : class, IExpanderInteractor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostProcessorInteractor{TExpander}"/> class.
        /// </summary>
        /// <param name="dependencyProvider"><seealso cref="IDependencyFactoryInteractor"/></param>
        protected PostProcessorInteractor(IDependencyFactoryInteractor dependencyProvider)
            : base(dependencyProvider)
        {
        }
    }
}
