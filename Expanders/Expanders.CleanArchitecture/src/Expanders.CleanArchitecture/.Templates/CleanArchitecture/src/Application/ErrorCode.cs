namespace NS.Application
{
    public sealed class ErrorCode
    {
        public ErrorCode(int code, string message)
        {
            this.Code = code;
            Message = message;
        }

        public int Code { get; }
        public string Message { get; }
    }
}
