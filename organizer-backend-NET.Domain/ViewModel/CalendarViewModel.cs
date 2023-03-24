using System.ComponentModel.DataAnnotations;

namespace organizer_backend_NET.Domain.ViewModel
{
    public class CalendarViewModel
    {

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EventStart { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EventEnd { get; set; }

        [StringLength(100, MinimumLength = 0)]
        public string Description { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 4)]
        public string Background { get; set; }
    }
}
