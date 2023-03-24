using organizer_backend_NET.Domain.Entity;
using organizer_backend_NET.Domain.Interfaces;
using organizer_backend_NET.Domain.ViewModel;

namespace organizer_backend_NET.Implements.Interfaces
{
    public interface IUserService
    {
        Task <IBaseResponse<User>> SignUp(SignupViewModel model);

        Task <IBaseResponse<User>> SignIn(SigninViewModel model);

        Task<IBaseResponse<User>> GetItem(int UId);

        Task<IBaseResponse<User>> RestoreItem(int UId);

        Task<IBaseResponse<bool>> RemoveItem(int UId);

        Task<IBaseResponse<User>> EditItem(int UId, SignupViewModel model);
    }
}
