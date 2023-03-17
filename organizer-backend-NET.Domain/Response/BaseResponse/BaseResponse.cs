using organizer_backend_NET.Domain.Enum;
using organizer_backend_NET.Domain.Interfaces.IResponse;

namespace organizer_backend_NET.Domain.Response.BaseResponse
{
    public class BaseResponse<T> : IBaseResponse<T>
    {
        public string? Descritption { get; set; }
        public EStatusCode StatusCode { get; set; }
        public T? Data { get; set; }
    }
}
