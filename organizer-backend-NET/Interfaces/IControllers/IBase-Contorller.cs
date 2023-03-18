using organizer_backend_NET.Domain.Interfaces;

namespace organizer_backend_NET.Interfaces.IControllers
{
    public interface IBase_Contorller<TD, VM>
    {
        public Task<IBaseResponse<IEnumerable<TD>>> GetAll();

        public Task<IBaseResponse<TD>> GetOne(int id);

        public Task<IBaseResponse<bool>> Create(VM model);

        public Task<IBaseResponse<TD>> Save(VM todo);

        public Task<IBaseResponse<bool>> Remove(int id);

        public Task<IBaseResponse<TD>> Restore(int id);
    }
}
