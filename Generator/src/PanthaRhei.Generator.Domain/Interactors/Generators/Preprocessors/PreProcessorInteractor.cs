using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Expanders;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Preprocessors
{
    /// <summary>
    /// An abstract implementation of the <see cref="PreProcessorInteractor{TExpander}"/>.
    /// </summary>
    /// <typeparam name="TExpander">A deriveded type of <see cref="IExpanderInteractor"/>.</typeparam>
    public abstract class PreProcessorInteractor<TExpander> : Processor<TExpander>, IPreProcessorInteractor<TExpander>
        where TExpander : class, IExpanderInteractor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PreProcessorInteractor{TExpander}"/> class.
        /// </summary>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        protected PreProcessorInteractor(IDependencyFactoryInteractor dependencyFactory)
            : base(dependencyFactory)
        {
        }
    }
}
