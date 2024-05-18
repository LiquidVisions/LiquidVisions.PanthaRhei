namespace LiquidVisions.PanthaRhei.Domain
{
    /// <summary>
    /// Class that contains the Fault Codes.
    /// </summary>
    public static class FaultCodes
    {
        private static readonly FaultCode badRequest = new(400, "Bad Request");
        private static readonly FaultCode unAuthorized = new(401, "Unauthorized");
        private static readonly FaultCode notFound = new(404, "NotFound");

        // Server Errors
        private static readonly FaultCode internalServerError = new(500, "Internal Server Error");

        /// <summary>
        /// Gets the Bad Request Fault Code.
        /// </summary>
        public static FaultCode BadRequest => badRequest;

        /// <summary>
        /// Gets the Unauthorized Fault Code.
        /// </summary>
        public static FaultCode UnAuthorized => unAuthorized;

        /// <summary>
        /// Gets the Not Found Fault Code.
        /// </summary>
        public static FaultCode NotFound => notFound;

        /// <summary>
        /// Gets the Internal Server Error Fault Code.
        /// </summary>
        public static FaultCode InternalServerError => internalServerError;

        // Client Errors

    }
}
