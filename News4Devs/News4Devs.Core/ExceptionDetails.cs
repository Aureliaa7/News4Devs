namespace News4Devs.Core
{
    public class ExceptionDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public ExceptionDetails() { }

        public ExceptionDetails(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
}
