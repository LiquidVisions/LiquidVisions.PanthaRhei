namespace LiquidVisions.PanthaRhei.Generator.Domain.Dependencies
{
    /// <summary>
    /// Specifies a contract for an object that allows dependency registration as part of a <seealso cref="IExpander"/>.
    /// </summary>
    public interface IExpanderDependencyManager
    {
        /// <summary>
        /// Registers all dependencies that are part of a specific <seealso cref="IExpander"/>.
        /// </summary>
        void Register();
    }
}
