using System.ComponentModel.DataAnnotations;

namespace organizer_backend_NET.Domain.ViewModel
{
    public class SigninViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
