using organizer_backend_NET.Domain.Entity;
using System.Net;

namespace organizer_backend_NET.Response
{
    public class LoginResponse
    {
        public HttpStatusCode Code { get; set; }

        public string? Message { get; set; }

        public User Data { get; set; }

        public string Token { get; set; }
    }
}
