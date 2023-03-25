using Microsoft.EntityFrameworkCore;
using organizer_backend_NET.DAL.Interfaces;
using organizer_backend_NET.Domain.Helpers;
using organizer_backend_NET.Domain.Entity;
using organizer_backend_NET.Domain.Interfaces;
using organizer_backend_NET.Domain.Response;
using organizer_backend_NET.Domain.ViewModel;
using organizer_backend_NET.Implements.Interfaces;
using organizer_backend_NET.Domain.Messages;
using System.Net;

namespace organizer_backend_NET.Implements.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository userRepository)
        {
            _repository = userRepository;
        }

        private async Task<User?> SearchUniqEmail(string Email)
        {
            return await _repository.Read().FirstOrDefaultAsync(item => item.Email == Email);
        }

        public async Task<IBaseResponse<User>> SignUp(SignupViewModel model)
        {
            try
            {
                DateTime timeStamp = DateTime.UtcNow;

                var uniqEmail = await SearchUniqEmail(model.Email);

                if (uniqEmail != null)
                {
                    return new BaseResponse<User>()
                    {
                        Description = AppMessages.EmailIsBusy,
                        StatusCode = HttpStatusCode.BadRequest,
                    };
                }

                var newItem = new User()
                {
                    Email = model.Email,
                    Name = model.Name,
                    UrlAvatar = $"{model.UrlAvatar}",
                    CreatedAt = timeStamp,
                    UpdatedAt = timeStamp,
                    Password = HashPasswordHelper.HashPassword(model.Password),
                };

                await _repository.Create(newItem);

                return new BaseResponse<User>()
                {
                    Description = AppMessages.CreateSucces,
                    StatusCode = HttpStatusCode.Created,
                    Data = newItem,
                };
            } catch (Exception ex)
            {
                return new BaseResponse<User>()
                {
                    Description = $"[Get] : {ex.Message}",
                    StatusCode = HttpStatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<User>> SignIn(SigninViewModel model)
        {
            try
            {
                var itemResponse = await _repository.Read().FirstOrDefaultAsync(item => item.Email == model.Email && item.DeleteAt == null);

                //! check password

                if (itemResponse != null)
                {
                    return new BaseResponse<User>()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Data = itemResponse,
                    };
                }

                return new BaseResponse<User>()
                {
                    Description = AppMessages.IncorrectEmilOrPassword,
                    StatusCode = HttpStatusCode.BadRequest,
                };
            } catch (Exception ex)
            {
                return new BaseResponse<User>()
                {
                    Description = $"[SignIn] : {ex.Message}",
                    StatusCode = HttpStatusCode.InternalServerError,
                };
            }
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
                        Description = AppMessages.UserNotFound,
                        StatusCode = HttpStatusCode.NotFound,
                    };
                }

                return new BaseResponse<User>()
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = itemResponse,
                };

            } catch (Exception ex)
            {
                return new BaseResponse<User>()
                {
                    Description = $"[GetItem] : {ex.Message}",
                    StatusCode = HttpStatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<bool>> RemoveItem(int UId)
        {
            try
            {
                var itemResponse = await _repository.Read().FirstOrDefaultAsync(item => item.UId == UId && item.DeleteAt == null);

                if (itemResponse == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = AppMessages.UserNotFound,
                        StatusCode = HttpStatusCode.NotFound,
                    };
                }

                itemResponse.DeleteAt = DateTime.UtcNow;
                await _repository.Update(itemResponse);

                return new BaseResponse<bool>()
                {
                    Description = AppMessages.RemoveSucces,
                    StatusCode = HttpStatusCode.OK,
                    Data = true,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[RemoveItem] : {ex.Message}",
                    StatusCode = HttpStatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<User>> RestoreItem(int UId)
        {
            try
            {
                var itemResponse = await _repository.Read().FirstOrDefaultAsync(item => item.UId == UId && item.DeleteAt != null);

                if (itemResponse == null)
                {
                    return new BaseResponse<User>()
                    {
                        Description = AppMessages.UserNotFound,
                        StatusCode = HttpStatusCode.NotFound,
                    };
                }

                itemResponse.DeleteAt = null;
                await _repository.Update(itemResponse);


                return new BaseResponse<User>()
                {
                    Description = AppMessages.RestoreSucces,
                    StatusCode = HttpStatusCode.OK,
                    Data = itemResponse,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<User>()
                {
                    Description = $"[RestoreItem] : {ex.Message}",
                    StatusCode = HttpStatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<User>> EditItem(int UId, SignupViewModel model)
        {
            try
            {
                var itemResponse = await _repository.Read().FirstOrDefaultAsync(item => item.UId == UId && item.DeleteAt == null);

                if (itemResponse == null)
                {
                    return new BaseResponse<User>()
                    {
                        Description = AppMessages.UserNotFound,
                        StatusCode = HttpStatusCode.NotFound,
                    };
                }

                if (itemResponse.Email != model.Email)
                {
                    var uniqEmail = await SearchUniqEmail(model.Email);

                    if (uniqEmail != null)
                    {
                        return new BaseResponse<User>()
                        {
                            Description = AppMessages.EmailIsBusy,
                            StatusCode = HttpStatusCode.BadRequest,
                        };
                    } else
                    {
                        itemResponse.Email = model.Email;
                    }
                }

                itemResponse.Name = model.Name;
                itemResponse.UrlAvatar = $"{model.UrlAvatar}";
                itemResponse.UpdatedAt = DateTime.UtcNow;
                //! itemResponse.Password = 

                var response = await _repository.Update(itemResponse);
                return new BaseResponse<User>()
                {
                    Description = AppMessages.UpdateSucces,
                    StatusCode = HttpStatusCode.OK,
                    Data = response,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<User>()
                {
                    Description = $"[RestoreItem] : {ex.Message}",
                    StatusCode = HttpStatusCode.InternalServerError,
                };
            }
        }
    }
}
