using organizer_backend_NET.Domain.Enums;
using organizer_backend_NET.Domain.Interfaces;

namespace organizer_backend_NET.Domain.Response
{
    public class BaseResponse<T> : IBaseResponse<T>
    {
        public string? Descritption { get; set; }
        public EStatusCode StatusCode { get; set; }
        public T? Data { get; set; }
    }
}
