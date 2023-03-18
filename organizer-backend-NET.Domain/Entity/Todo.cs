using organizer_backend_NET.Domain.Enum;
using organizer_backend_NET.Domain.Interfaces.IBaseEntity;
using organizer_backend_NET.Domain.Interfaces.IUId;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace organizer_backend_NET.Domain.Entity
{
    [Table("Todos")]
    public class Todo : IBaseEntity, IUId
    {
        [Column("Id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("UId"), ForeignKey("User")]
        public int UId { get; set; }

        [Column("Name"), MaxLength(59)]
        public string Name { get; set; }

        [Column("Category"), MaxLength(30)]
        public string Category { get; set; }

        [Column("Priority")]
        public EPriority Priority { get; set; }

        [Column("DeadLine")]
        public DateTime DeadLine { get; set; }

        [Column("Status")]
        public bool Status { get; set; }

        [Column("Background"), MaxLength(20)]
        public string Background { get; set; }

        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; }

        [Column("UpdatedAt")]
        public DateTime UpdatedAt { get; set; }

        [Column("DeleteAt")]
        public DateTime? DeleteAt { get; set; }

        public virtual User User { get; set; }
    }
}
