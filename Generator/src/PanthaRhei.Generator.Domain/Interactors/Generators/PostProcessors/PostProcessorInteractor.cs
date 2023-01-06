using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Expanders;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.PostProcessors
{
    /// <summary>
    /// An abstract implementation of the <see cref="Processor{TExpander}"/>.
    /// </summary>
    /// <typeparam name="TExpander">A deriveded type of <see cref="IExpanderInteractor"/>.</typeparam>
    internal abstract class PostProcessorInteractor<TExpander> : Processor<TExpander>, IPostProcessorInteractor<TExpander>
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
