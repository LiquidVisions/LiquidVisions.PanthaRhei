namespace LiquidVisions.PanthaRhei.Generator.Application.Boundaries
{
    /// <summary>
    /// Specifies the contract for the runtime application service.
    /// </summary>
    public interface ICodeGeneratorBoundary
    {
        /// <summary>
        /// Executes the code generation request.
        /// </summary>
        void Execute();
    }
}
