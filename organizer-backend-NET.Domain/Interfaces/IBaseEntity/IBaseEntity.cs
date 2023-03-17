
using organizer_backend_NET.DAL.Interfaces.ITiming;

namespace organizer_backend_NET.Domain.Interfaces.IBaseEntity
{
    public interface IBaseEntity: ITiming
    {
        public int Id { get; set; }
    }
}
