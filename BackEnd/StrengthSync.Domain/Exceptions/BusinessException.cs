namespace StrengthSync.Domain.Exceptions
{
    public class BusinessException(ErrorCodes errorCode, string message) : Exception(message)
    {
        public ErrorCodes ErrorCode { get; } = errorCode;
    }
}
