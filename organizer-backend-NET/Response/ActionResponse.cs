using System.Net;

namespace organizer_backend_NET.Response
{
    public class ActionResponse<T> {
        public HttpStatusCode Code { get; set; }

        public string? Message { get; set; }

        public T? Data { get; set; }
    }
}
