using System.ComponentModel.DataAnnotations;

namespace organizer_backend_NET.Domain.ViewModel.Todo
{
    public class TodoViewModel
    {
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Required]
        public string Name { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Required]
        public string Category { get; set; }

        [StringLength(1, MinimumLength = 1)]
        [Required]
        public string Priority { get; set; }

        [DataType(DataType.DateTime)]
        [Required]
        public DateTime DeadLine { get; set; }

        [Required]
        public bool Status { get; set; }

        [StringLength(20, MinimumLength = 4)]
        [Required]
        public string Background { get; set; }
    }
}
