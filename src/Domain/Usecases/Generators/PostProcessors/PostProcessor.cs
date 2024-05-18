using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Expanders;

namespace LiquidVisions.PanthaRhei.Domain.Usecases.Generators.PostProcessors
{
    /// <summary>
    /// An abstract implementation of the <see cref="Processor{TExpander}"/>.
    /// </summary>
    /// <typeparam name="TExpander">A derived type of <see cref="IExpander"/>.</typeparam>
    /// <remarks>
    /// Initializes a new instance of the <see cref="PostProcessor{TExpander}"/> class.
    /// </remarks>
    /// <param name="dependencyProvider"><seealso cref="IDependencyFactory"/></param>
    public abstract class PostProcessor<TExpander>(IDependencyFactory dependencyProvider) : Processor<TExpander>(dependencyProvider), IPostProcessor<TExpander>
        where TExpander : class, IExpander
    {
    }
}
