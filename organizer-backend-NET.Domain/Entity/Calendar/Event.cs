namespace organizer_backend_NET.Domain.Entity.Calendar
{
    public class Event
    {
        public int uid { get; set; }

        public DateTime eventStart { get; set; }

        public DateTime eventEnd { get; set; }

        public string title { get; set; }

        public string description { get; set; }

        public string background { get; set; }

    }
}
