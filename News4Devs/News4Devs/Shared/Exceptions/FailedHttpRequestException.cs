using System;

namespace News4Devs.Shared.Exceptions
{
    public class FailedHttpRequestException : Exception
    {
        public FailedHttpRequestException(string message) : base(message)
        {
        }

        public FailedHttpRequestException() : base()
        {
        }

        public FailedHttpRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
