using System.ComponentModel.DataAnnotations;

namespace organizer_backend_NET.Domain.ViewModel
{
    public class TodoViewModel
    {

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Category { get; set; }

        [Required]
        public int Priority { get; set; } //!

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DeadLine { get; set; }

        [Required]
        public bool Status { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 4)]
        public string Background { get; set; }
    }
}
