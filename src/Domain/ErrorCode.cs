namespace LiquidVisions.PanthaRhei.Domain
{
    public sealed class ErrorCode
    {
        public ErrorCode(int code, string message)
        {
            Code = code;
            Message = message;
        }

        public int Code { get; }
        public string Message { get; }
    }
}
