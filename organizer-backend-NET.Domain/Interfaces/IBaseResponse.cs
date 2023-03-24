using organizer_backend_NET.Domain.Enums;

namespace organizer_backend_NET.Domain.Interfaces
{
    public interface IBaseResponse<T>
    {
        public string? Descritption { get; set; }

        public EStatusCode StatusCode { get; set; }

        public T Data { get; set; }
    }
}
