namespace LiquidVisions.PanthaRhei.Domain
{
    /// <summary>
    /// Representation of an Error.
    /// </summary>
    public sealed class Fault
    {
        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        public FaultCode FaultCode { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        public string FaultMessage { get; set; }
    }
}
