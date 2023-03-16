using System.ComponentModel.DataAnnotations;

namespace organizer_backend_NET.Domain.ViewModel.Todo
{
    public class TodoViewModel
    {
        public int Id { get; set; }

        [StringLength(20, MinimumLength = 3)]
        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }

        [StringLength(20, MinimumLength = 3)]
        [Display(Name = "Category")]
        [Required]
        public string Category { get; set; }

        [StringLength(1, MinimumLength = 1)]
        [Display(Name = "Priority")]
        [Required]
        public string Priority { get; set; }

        [Display(Name = "DeadLine")]
        [DataType(DataType.DateTime)]
        [Required]
        public DateTime DeadLine { get; set; }

        [Display(Name = "Status")]
        [Required]
        public bool Status { get; set; }

        [StringLength(10, MinimumLength = 4)]
        [Display(Name = "Background")]
        [Required]
        public string Background { get; set; }
    }
}
