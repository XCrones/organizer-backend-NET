using organizer_backend_NET.Domain.Enum;

namespace organizer_backend_NET.Domain.Interfaces.IResponse
{
    public interface IBaseResponse<T>
    {
        public string? Descritption { get; set; }

        public TStatusCode StatusCode { get; set; }

        public T Data { get; set; }
    }
}
