using Microsoft.EntityFrameworkCore;
using organizer_backend_NET.DAL.Interfaces;
using organizer_backend_NET.Domain.Common;
using organizer_backend_NET.Domain.Entity;
using organizer_backend_NET.Domain.Enum;
using organizer_backend_NET.Domain.Interfaces;
using organizer_backend_NET.Domain.Response;
using organizer_backend_NET.Domain.ViewModel;
using organizer_backend_NET.Implements.Interfaces;

namespace organizer_backend_NET.Implements.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository userRepository)
        {
            _repository = userRepository;
        }

        public async Task<IBaseResponse<User>> SignUp(SignupViewModel model)
        {
            try
            {
                DateTime timeStamp = DateTime.UtcNow;

                var itemResponse = await _repository.Read().FirstOrDefaultAsync(item => item.Email == model.Email && item.DeleteAt == null);

                if (itemResponse != null)
                {
                    return new BaseResponse<User>()
                    {
                        Descritption = ResponseMessage.EMAIL_IS_BUSY,
                        StatusCode = EStatusCode.BadRequest,
                    };
                }

                var newItem = new User()
                {
                    Email = model.Email,
                    Name = model.Name,
                    UrlAvatar = model.UrlAvatar,
                    CreatedAt = timeStamp,
                    UpdatedAt = timeStamp,
                    Password = HashPasswordCommon.HashPassword(model.Password),
                };

                await _repository.Create(newItem);


                return new BaseResponse<User>()
                {
                    Descritption = ResponseMessage.CREATE_SUCCES,
                    StatusCode = EStatusCode.OK,
                    Data = newItem,
                };
            } catch (Exception ex)
            {
                return new BaseResponse<User>()
                {
                    Descritption = $"[Get] : {ex.Message}",
                    StatusCode = EStatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<User>> SignIn(SigninViewModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<IBaseResponse<User>> GetItem(int UId)
        {
            try
            {
                var itemResponse = await _repository.Read().FirstOrDefaultAsync(item => item.UId == UId && item.DeleteAt == null);

                if (itemResponse == null)
                {
                    return new BaseResponse<User>()
                    {
                        Descritption = ResponseMessage.NOT_FOUND,
                        StatusCode = EStatusCode.NotFound,
                    };
                }

                return new BaseResponse<User>()
                {
                    StatusCode = EStatusCode.OK,
                    Data = itemResponse,
                };

            } catch (Exception ex)
            {
                return new BaseResponse<User>()
                {
                    Descritption = $"[GetItem] : {ex.Message}",
                    StatusCode = EStatusCode.InternalServerError,
                };
            }
        }

        public Task<IBaseResponse<bool>> RemoveItem(int UId)
        {
            throw new NotImplementedException();
        }

        public Task<IBaseResponse<User>> RestoreItem(int UId)
        {
            throw new NotImplementedException();
        }
    }
}
