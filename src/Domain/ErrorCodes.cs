namespace LiquidVisions.PanthaRhei.Domain
{
    public sealed class ErrorCodes
    {
        // Client Errors
        public readonly static ErrorCode BadRequest = new(400, "Bad Request");
        public readonly static ErrorCode UnAuthorized = new(401, "Unauthorized");
        public readonly static ErrorCode NotFound = new(404, "NotFound");

        // Server Errors
        public readonly static ErrorCode InternalServerError = new(500, "Internal Server Error");
    }
}
