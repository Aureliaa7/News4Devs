using System.Net;

namespace News4Devs.Shared.Models
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }

        public HttpStatusCode StatusCode { get; set; }
    }
}
