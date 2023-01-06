namespace LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators
{
    /// <summary>
    /// Represents a contract of an object that is able to build a <see cref="ICodeGeneratorInteractor"/>.
    /// </summary>
    public interface ICodeGeneratorBuilderInteractor
    {
        /// <summary>
        /// Executes the building command.
        /// </summary>
        /// <returns><seealso cref="ICodeGeneratorInteractor"/></returns>
        ICodeGeneratorInteractor Build();
    }
}
