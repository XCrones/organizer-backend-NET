
using organizer_backend_NET.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace organizer_backend_NET.Domain.Entity
{
    [Table("Calendars")]
    public class Calendar : IBaseEntity, IUId
    {
        [Column("Id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("UId"), ForeignKey("User")]
        public int UId { get; set; }

        [Column("Name"), MaxLength(50)]
        public string Name { get; set; }

        [Column("EventStart")]
        public DateTime EventStart { get; set; }

        [Column("EventEnd")]
        public DateTime EventEnd { get; set; }

        [Column("Description"), MaxLength(100)]
        public string Description { get; set; }

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
