using organizer_backend_NET.DAL.Interfaces.ITiming;
using organizer_backend_NET.Domain.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace organizer_backend_NET.Domain.Entity.Todo
{
    [Table("Todo")]
    public class Todo : ITiming
    {
        public int Id { get; set; }
        public int Uid { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public EPriority Priority { get; set; }
        public DateTime DeadLine { get; set; }
        public bool Status { get; set; }
        public string Background { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeleteAt { get; set; }
    }
}
