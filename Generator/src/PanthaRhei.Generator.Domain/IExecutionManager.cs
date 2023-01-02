namespace LiquidVisions.PanthaRhei.Generator.Domain
{
    /// <summary>
    /// Specifice an interface for an object that needs to be able to execute commands.
    /// </summary>
    public interface IExecutionManager
    {
        /// <summary>
        /// Gets a value indicating whether the handler should be executed.
        /// </summary>
        bool CanExecute { get; }

        /// <summary>
        /// Executes the handler.
        /// </summary>
        void Execute();
    }
}
