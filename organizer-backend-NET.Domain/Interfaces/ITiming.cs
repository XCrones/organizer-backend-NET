namespace organizer_backend_NET.Domain.Interfaces
{
    public interface ITiming
    {
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
        DateTime? DeleteAt { get; set; }
    }
}
