namespace LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Expanders
{
    /// <summary>
    /// Specifies a contract for an object that allows dependency registration as part of a <seealso cref="IExpanderInteractor"/>.
    /// </summary>
    public interface IExpanderDependencyManagerInteractor
    {
        /// <summary>
        /// Registers all dependencies that are part of a specific <seealso cref="IExpanderInteractor"/>.
        /// </summary>
        void Register();
    }
}
