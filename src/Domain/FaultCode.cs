namespace LiquidVisions.PanthaRhei.Domain
{
    /// <summary>
    /// Representation of a single Fault Code.
    /// </summary>
    public sealed class FaultCode(int code, string message)
    {
        /// <summary>
        /// Gets the code of the Fault.
        /// </summary>
        public int Code { get; } = code;

        /// <summary>
        /// Gets the message of the Fault.
        /// </summary>
        public string Message { get; } = message;
    }
}
