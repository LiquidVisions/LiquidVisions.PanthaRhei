namespace LiquidVisions.PanthaRhei.Domain.Usecases.Generators
{
    /// <summary>
    /// Specifies an interface for an object that needs to be able to execute commands.
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

    /// <summary>
    /// Specifies an interface for an object that needs to be able to execute commands.
    /// </summary>
    /// <typeparam name="TCommandModel">The Command Model</typeparam>
    public interface ICommand<TCommandModel>
    {
        /// <summary>
        /// Gets a value indicating whether the handler should be executed.
        /// </summary>
        bool Enabled { get; }

        /// <summary>
        /// Executes the handler.
        /// </summary>
        /// <param name="model">The Command Model.</param>
        void Execute(TCommandModel model);
    }
}
