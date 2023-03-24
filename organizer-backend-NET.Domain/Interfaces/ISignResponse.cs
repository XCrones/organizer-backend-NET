
namespace organizer_backend_NET.Domain.Interfaces
{
    public interface ISignResponse<T>
    {
        public T? UserData { get; set; }

        public string? Token { get; set; }

        public string? Description { get; set; }
    }
}
