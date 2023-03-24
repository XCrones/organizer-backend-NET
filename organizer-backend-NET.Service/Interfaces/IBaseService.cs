using organizer_backend_NET.Domain.Interfaces;

namespace organizer_backend_NET.Implements.Interfaces
{
    public interface IBaseService<TD, VM>
    {
        Task<IBaseResponse<IEnumerable<TD>>> GetAll(int UId);

        Task<IBaseResponse<TD>> GetItemById(int UId, int id);

        Task<IBaseResponse<TD>> GetItemByName(int UId, string name);

        Task<IBaseResponse<bool>> RemoveItem(int UId, int id);

        Task<IBaseResponse<bool>> CreateItem(int UId, VM model);

        Task<IBaseResponse<TD>> EditItem(int UId, int id, VM model);

        Task<IBaseResponse<TD>> RestoreItem(int UId, int id);
    }
}
