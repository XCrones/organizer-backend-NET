using organizer_backend_NET.Domain.Enums;
using System.Net;

namespace organizer_backend_NET.Domain.Interfaces
{
    public interface IBaseResponse<T>
    {
        public string? Description { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public T Data { get; set; }
    }
}
