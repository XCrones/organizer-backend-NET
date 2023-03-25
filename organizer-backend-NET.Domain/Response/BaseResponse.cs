using organizer_backend_NET.Domain.Interfaces;
using System.Net;

namespace organizer_backend_NET.Domain.Response
{
    public class BaseResponse<T> : IBaseResponse<T>
    {
        public string? Description { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public T? Data { get; set; }
    }
}
