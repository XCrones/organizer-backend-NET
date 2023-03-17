using organizer_backend_NET.DAL.Interfaces.ITiming;

namespace organizer_backend_NET.Domain.Entity.User
{
    public class User : ITiming
    {
        public int UId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string UrlAvatar { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime? DeleteAt { get; set; }
    }
}
