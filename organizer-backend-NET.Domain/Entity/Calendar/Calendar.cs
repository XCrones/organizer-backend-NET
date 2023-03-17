using organizer_backend_NET.Domain.Interfaces.IBaseEntity;

namespace organizer_backend_NET.Domain.Entity.Calendar
{
    public class Calendar : IBaseEntity
    {
        public int Id { get; set; }

        public int Uid { get; set; }

        public string Name { get; set; }

        public DateTime EventStart { get; set; }

        public DateTime EventEnd { get; set; }

        public string Description { get; set; }

        public string Background { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime? DeleteAt { get; set; }
    }
}
