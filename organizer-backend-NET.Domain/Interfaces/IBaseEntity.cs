namespace organizer_backend_NET.Domain.Interfaces
{
    public interface IBaseEntity : ITiming
    {
        public int Id { get; set; }
    }
}
