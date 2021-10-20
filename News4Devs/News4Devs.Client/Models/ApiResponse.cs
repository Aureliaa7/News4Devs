using System.Net;

namespace News4Devs.Client.Models
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }

        public HttpStatusCode StatusCode { get; set; }
    }
}
