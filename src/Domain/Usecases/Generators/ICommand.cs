namespace LiquidVisions.PanthaRhei.Domain.Usecases.Generators
{
    /// <summary>
    /// Specifice an interface for an object that needs to be able to execute commands.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Gets a value indicating whether the handler should be executed.
        /// </summary>
        bool Enabled { get; }

        /// <summary>
        /// Executes the handler.
        /// </summary>
        void Execute();
    }
}
