using LiquidVisions.PanthaRhei.Generator.Domain.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Generators.Expanders;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Generators.Preprocessors
{
    /// <summary>
    /// An abstract implementation of the <see cref="PreProcessor{TExpander}"/>.
    /// </summary>
    /// <typeparam name="TExpander">A deriveded type of <see cref="IExpander"/>.</typeparam>
    public abstract class PreProcessor<TExpander> : Processor<TExpander>, IPreProcessor<TExpander>
        where TExpander : class, IExpander
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PreProcessor{TExpander}"/> class.
        /// </summary>
        /// <param name="dependencyResolver"><seealso cref="IDependencyResolver"/></param>
        protected PreProcessor(IDependencyResolver dependencyResolver)
            : base(dependencyResolver)
        {
        }
    }
}
