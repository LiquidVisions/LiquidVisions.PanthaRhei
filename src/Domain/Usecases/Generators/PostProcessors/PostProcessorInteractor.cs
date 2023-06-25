using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Expanders;

namespace LiquidVisions.PanthaRhei.Domain.Usecases.Generators.PostProcessors
{
    /// <summary>
    /// An abstract implementation of the <see cref="Processor{TExpander}"/>.
    /// </summary>
    /// <typeparam name="TExpander">A deriveded type of <see cref="IExpander"/>.</typeparam>
    public abstract class PostProcessorInteractor<TExpander> : Processor<TExpander>, IPostProcessorInteractor<TExpander>
        where TExpander : class, IExpander
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostProcessorInteractor{TExpander}"/> class.
        /// </summary>
        /// <param name="dependencyProvider"><seealso cref="IDependencyFactory"/></param>
        protected PostProcessorInteractor(IDependencyFactory dependencyProvider)
            : base(dependencyProvider)
        {
        }
    }
}
