namespace LiquidVisions.PanthaRhei.Application.Boundaries
{
    /// <summary>
    /// Specifies the contract for the runtime application service.
    /// </summary>
    public interface IExpandBoundary
    {
        /// <summary>
        /// Executes the code generation request.
        /// </summary>
        void Execute();
    }
}
