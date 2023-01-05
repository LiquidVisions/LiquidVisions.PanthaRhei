using LiquidVisions.PanthaRhei.Generator.Domain.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Generators.Expanders;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Generators.PostProcessors
{
    /// <summary>
    /// An abstract implementation of the <see cref="Processor{TExpander}"/>.
    /// </summary>
    /// <typeparam name="TExpander">A deriveded type of <see cref="IExpander"/>.</typeparam>
    public abstract class PostProcessor<TExpander> : Processor<TExpander>, IPostProcessor<TExpander>
        where TExpander : class, IExpander
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostProcessor{TExpander}"/> class.
        /// </summary>
        /// <param name="dependencyProvider"><seealso cref="IDependencyResolver"/></param>
        protected PostProcessor(IDependencyResolver dependencyProvider)
            : base(dependencyProvider)
        {
        }
    }
}
