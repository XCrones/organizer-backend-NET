using organizer_backend_NET.Domain.Enums;
using organizer_backend_NET.Domain.Response;
using Microsoft.EntityFrameworkCore;
using organizer_backend_NET.Domain.Helpers;
using organizer_backend_NET.Domain.Entity;
using organizer_backend_NET.Implements.Interfaces;
using organizer_backend_NET.Domain.ViewModel;
using organizer_backend_NET.Domain.Interfaces;
using organizer_backend_NET.DAL.Interfaces;

namespace organizer_backend_NET.Implements.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRespository _repository;

        public TodoService(ITodoRespository todoRespository)
        {
            _repository = todoRespository;
        }

        public async Task<IBaseResponse<bool>> CreateItem(TodoViewModel viewModel)
        {
            try
            {
                DateTime timeStamp = DateTime.UtcNow;

                var newItem = new Todo()
                {
                    UId = 1, //!
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

                return new BaseResponse<bool>()
                {
                    Descritption = nameof(EMessage.create_succes),
                    StatusCode = EStatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Descritption = $"[CreateItem] : {ex.Message}",
                    StatusCode = EStatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<bool>> RemoveItem(int id)
        {
            try
            {
                var itemResponse = await _repository.Read().FirstOrDefaultAsync(item => item.Id == id && item.DeleteAt == null);

                if (itemResponse == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Descritption = nameof(EMessage.not_found),
                        StatusCode = EStatusCode.NotFound,
                    };
                }

                itemResponse.DeleteAt = DateTime.UtcNow;
                await _repository.Update(itemResponse);

                return new BaseResponse<bool>()
                {
                    Descritption = nameof(EMessage.delete_succes),
                    StatusCode = EStatusCode.OK,
                    Data = true,
                };

            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Descritption = $"[DeleteItem] : {ex.Message}",
                    StatusCode = EStatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<Todo>> EditItem(int id, TodoViewModel viewModel)
        {
            try
            {
                var itemResponse = await _repository.Read().FirstOrDefaultAsync(item => item.Id == id && item.DeleteAt == null);

                if (itemResponse == null)
                {
                    return new BaseResponse<Todo>()
                    {
                        Descritption = nameof(EMessage.not_found),
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
                    Descritption = nameof(EMessage.update_succes),
                    StatusCode = EStatusCode.Edited,
                    Data = response,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Todo>()
                {
                    Descritption = $"[EditItem] : {ex.Message}",
                    StatusCode = EStatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<Todo>>> GetAll()
        {
            try
            {
                var itemsResponse = await _repository.Read().Where(item => item.DeleteAt == null).ToListAsync();

                if (itemsResponse == null)
                {
                    return new BaseResponse<IEnumerable<Todo>>()
                    {
                        Descritption = nameof(EMessage.not_found),
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
                    Descritption = $"[GetAll] : {ex.Message}",
                    StatusCode = EStatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<Todo>> GetItemById(int id)
        {
            try
            {
                var itemResponse = await _repository.Read().FirstOrDefaultAsync(item => item.Id == id && item.DeleteAt == null);

                if (itemResponse == null)
                {
                    return new BaseResponse<Todo>()
                    {
                        Descritption = nameof(EMessage.not_found),
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
                    Descritption = $"[GetItemById] : {ex.Message}",
                    StatusCode = EStatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<Todo>> GetItemByName(string name)
        {
            try
            {
                var itemResponse = await _repository.Read().FirstOrDefaultAsync(item => item.Name == name && item.DeleteAt == null);

                if (itemResponse == null)
                {
                    return new BaseResponse<Todo>()
                    {
                        Descritption = nameof(EMessage.not_found),
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
                    Descritption = $"[GetItemByName] : {ex.Message}",
                    StatusCode = EStatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<Todo>> RestoreItem(int id)
        {
            try
            {
                var itemResponse = await _repository.Read().FirstOrDefaultAsync(item => item.Id == id && item.DeleteAt != null);

                if (itemResponse == null)
                {
                    return new BaseResponse<Todo>()
                    {
                        Descritption = nameof(EMessage.not_found),
                        StatusCode = EStatusCode.NotFound,
                    };
                }

                itemResponse.DeleteAt = null;
                await _repository.Update(itemResponse);

                return new BaseResponse<Todo>()
                {
                    Descritption = nameof(EMessage.restore_succes),
                    StatusCode = EStatusCode.OK,
                    Data = itemResponse,
                };

            }
            catch (Exception ex)
            {
                return new BaseResponse<Todo>()
                {
                    Descritption = $"[RestoreItem] : {ex.Message}",
                    StatusCode = EStatusCode.InternalServerError,
                };
            }
        }
    }
}
