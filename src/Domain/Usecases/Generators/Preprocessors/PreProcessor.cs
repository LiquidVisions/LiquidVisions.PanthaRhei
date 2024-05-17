using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Expanders;

namespace LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Preprocessors
{
    /// <summary>
    /// An abstract implementation of the <see cref="PreProcessor{TExpander}"/>.
    /// </summary>
    /// <typeparam name="TExpander">A derived type of <see cref="IExpander"/>.</typeparam>
    /// <remarks>
    /// Initializes a new instance of the <see cref="PreProcessor{TExpander}"/> class.
    /// </remarks>
    /// <param name="dependencyFactory"><seealso cref="IDependencyFactory"/></param>
    public abstract class PreProcessor<TExpander>(IDependencyFactory dependencyFactory) : Processor<TExpander>(dependencyFactory), IPreProcessor<TExpander>
        where TExpander : class, IExpander
    {
    }
}
