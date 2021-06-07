using System;

namespace UserManagementAPI.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public ErrorCodes ErrorCode { get; }
        public string Details { get; set; }

        public UnauthorizedException(ErrorCodes errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
            Details = message;
        }
    }
}
