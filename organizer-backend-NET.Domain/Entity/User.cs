using organizer_backend_NET.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace organizer_backend_NET.Domain.Entity
{
    [Table("Users")]
    public class User : ITiming
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UId { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        [Column("Password")]
        public string Password { get; set; }

        [Column("UrlAvatar")]
        public string UrlAvatar { get; set; }

        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; }

        [Column("UpdatedAt")]
        public DateTime UpdatedAt { get; set; }

        [Column("DeleteAt")]
        public DateTime? DeleteAt { get; set; }

        public virtual ICollection<Todo> Todo { get; set; }

        public virtual ICollection<Calendar> Calendar { get; set; }

        public virtual ICollection<WeatherUsers> WeatherUser { get; set; }
    }
}
