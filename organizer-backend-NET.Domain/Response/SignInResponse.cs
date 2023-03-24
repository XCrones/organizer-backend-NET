using organizer_backend_NET.Domain.Interfaces;

namespace organizer_backend_NET.Domain.Response
{
    public class SignInResponse<T> : ISignResponse<T>
    {
        public T UserData { get; set; }

        public string Token { get; set; }

        public string? Description { get; set; }
    }
}
