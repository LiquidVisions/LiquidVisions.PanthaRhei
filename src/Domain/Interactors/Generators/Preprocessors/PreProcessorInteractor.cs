using LiquidVisions.PanthaRhei.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Interactors.Generators.Expanders;

namespace LiquidVisions.PanthaRhei.Domain.Interactors.Generators.Preprocessors
{
    /// <summary>
    /// An abstract implementation of the <see cref="PreProcessorInteractor{TExpander}"/>.
    /// </summary>
    /// <typeparam name="TExpander">A deriveded type of <see cref="IExpander"/>.</typeparam>
    public abstract class PreProcessorInteractor<TExpander> : Processor<TExpander>, IPreProcessorInteractor<TExpander>
        where TExpander : class, IExpander
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PreProcessorInteractor{TExpander}"/> class.
        /// </summary>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
        protected PreProcessorInteractor(IDependencyFactory dependencyFactory)
            : base(dependencyFactory)
        {
        }
    }
}
