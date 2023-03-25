using organizer_backend_NET.Domain.Enums;
using organizer_backend_NET.Domain.Response;
using Microsoft.EntityFrameworkCore;
using organizer_backend_NET.Domain.Helpers;
using organizer_backend_NET.Domain.Entity;
using organizer_backend_NET.Implements.Interfaces;
using organizer_backend_NET.Domain.ViewModel;
using organizer_backend_NET.Domain.Interfaces;
using organizer_backend_NET.DAL.Interfaces;
using organizer_backend_NET.Domain.Messages;

namespace organizer_backend_NET.Implements.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRespository _repository;

        public TodoService(ITodoRespository todoRespository)
        {
            _repository = todoRespository;
        }

        public async Task<IBaseResponse<Todo>> CreateItem(int UId ,TodoViewModel viewModel)
        {
            try
            {
                DateTime timeStamp = DateTime.UtcNow;

                var newItem = new Todo()
                {
                    UId = UId,
                    Name = viewModel.Name,
                    Background = viewModel.Background,
                    Category = viewModel.Category,
                    Status = false,
                    DeadLine = viewModel.DeadLine,
                    Priority = ConvertEnum.PriorityIntToEnum(viewModel.Priority),
                    UpdatedAt = timeStamp,
                    CreatedAt = timeStamp,
                };

                await _repository.Create(newItem);

                var result = await _repository.Read().FirstOrDefaultAsync(item => item.UId == newItem.UId && item.CreatedAt == timeStamp && item.DeleteAt == null);

                if (result == null)
                {
                    return new BaseResponse<Todo>()
                    {
                        StatusCode = EStatusCode.InternalServerError,
                        Description = AppMessages.NewItemNotFound,
                    };
                }

                return new BaseResponse<Todo>()
                {
                    StatusCode = EStatusCode.OK,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Todo>()
                {
                    Description = $"[CreateItem] : {ex.Message}",
                    StatusCode = EStatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<bool>> RemoveItem(int UId, int id)
        {
            try
            {
                var itemResponse = await _repository.Read().FirstOrDefaultAsync(item => item.UId == UId && item.Id == id && item.DeleteAt == null);

                if (itemResponse == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = AppMessages.NotFound,
                        StatusCode = EStatusCode.NotFound,
                    };
                }

                itemResponse.DeleteAt = DateTime.UtcNow;
                await _repository.Update(itemResponse);

                return new BaseResponse<bool>()
                {
                    Description = AppMessages.RemoveSucces,
                    StatusCode = EStatusCode.OK,
                    Data = true,
                };

            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteItem] : {ex.Message}",
                    StatusCode = EStatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<Todo>> EditItem(int UId, int id, TodoViewModel viewModel)
        {
            try
            {
                var itemResponse = await _repository.Read().FirstOrDefaultAsync(item => item.UId == UId && item.Id == id && item.DeleteAt == null);

                if (itemResponse == null)
                {
                    return new BaseResponse<Todo>()
                    {
                        Description = AppMessages.NotFound,
                        StatusCode = EStatusCode.NotFound,
                    };
                }

                itemResponse.Status = viewModel.Status;
                itemResponse.Name = viewModel.Name;
                itemResponse.Priority =  ConvertEnum.PriorityIntToEnum(viewModel.Priority);
                itemResponse.Background = viewModel.Background;
                itemResponse.DeadLine = viewModel.DeadLine;
                itemResponse.Category = viewModel.Category;
                itemResponse.UpdatedAt = DateTime.UtcNow;

                var response = await _repository.Update(itemResponse);

                return new BaseResponse<Todo>()
                {
                    Description = AppMessages.UpdateSucces,
                    StatusCode = EStatusCode.OK,
                    Data = itemResponse,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Todo>()
                {
                    Description = $"[EditItem] : {ex.Message}",
                    StatusCode = EStatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<Todo>>> GetAll(int UId)
        {
            try
            {
                var itemsResponse = await _repository.Read().Where(item => item.UId == UId && item.DeleteAt == null).ToListAsync();

                if (itemsResponse == null)
                {
                    return new BaseResponse<IEnumerable<Todo>>()
                    {
                        Description = AppMessages.NotFound,
                        StatusCode = EStatusCode.NotFound,
                    };
                }

                return new BaseResponse<IEnumerable<Todo>>()
                {
                    StatusCode = EStatusCode.OK,
                    Data = itemsResponse,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Todo>>()
                {
                    Description = $"[GetAll] : {ex.Message}",
                    StatusCode = EStatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<Todo>> GetItemById(int UId, int id)
        {
            try
            {
                var itemResponse = await _repository.Read().FirstOrDefaultAsync(item => item.UId == UId && item.Id == id && item.DeleteAt == null);

                if (itemResponse == null)
                {
                    return new BaseResponse<Todo>()
                    {
                        Description = AppMessages.NotFound,
                        StatusCode = EStatusCode.NotFound,
                    };
                }

                return new BaseResponse<Todo>()
                {
                    StatusCode = EStatusCode.OK,
                    Data = itemResponse,
                };

            }
            catch (Exception ex)
            {
                return new BaseResponse<Todo>()
                {
                    Description = $"[GetItemById] : {ex.Message}",
                    StatusCode = EStatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<Todo>> GetItemByName(int UId, string name)
        {
            try
            {
                var itemResponse = await _repository.Read().FirstOrDefaultAsync(item => item.UId == UId && item.Name == name && item.DeleteAt == null);

                if (itemResponse == null)
                {
                    return new BaseResponse<Todo>()
                    {
                        Description = AppMessages.NotFound,
                        StatusCode = EStatusCode.NotFound,
                    };
                }

                return new BaseResponse<Todo>()
                {
                    StatusCode = EStatusCode.OK,
                    Data = itemResponse,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Todo>()
                {
                    Description = $"[GetItemByName] : {ex.Message}",
                    StatusCode = EStatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<Todo>> RestoreItem(int UId, int id)
        {
            try
            {
                var itemResponse = await _repository.Read().FirstOrDefaultAsync(item => item.UId == UId && item.Id == id && item.DeleteAt != null);

                if (itemResponse == null)
                {
                    return new BaseResponse<Todo>()
                    {
                        Description = AppMessages.NotFound,
                        StatusCode = EStatusCode.NotFound,
                    };
                }

                itemResponse.DeleteAt = null;
                await _repository.Update(itemResponse);

                return new BaseResponse<Todo>()
                {
                    Description = AppMessages.RestoreSucces,
                    StatusCode = EStatusCode.OK,
                    Data = itemResponse,
                };

            }
            catch (Exception ex)
            {
                return new BaseResponse<Todo>()
                {
                    Description = $"[RestoreItem] : {ex.Message}",
                    StatusCode = EStatusCode.InternalServerError,
                };
            }
        }
    }
}
