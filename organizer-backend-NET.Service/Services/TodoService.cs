using organizer_backend_NET.DAL.Interfaces.ITodo;
using organizer_backend_NET.Domain.Interfaces.IResponse;
using organizer_backend_NET.Domain.ViewModel.Todo;
using organizer_backend_NET.Domain.Enum;
using organizer_backend_NET.Domain.Response.BaseResponse;
using Microsoft.EntityFrameworkCore;
using organizer_backend_NET.Domain.Common;
using organizer_backend_NET.Domain.Entity;
using organizer_backend_NET.Service.Interfaces;

namespace organizer_backend_NET.Service.Services
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
                    Name = viewModel.Name,
                    Background = viewModel.Background,
                    Category = viewModel.Category,
                    Status = false,
                    UId = 1, //!
                    DeadLine = viewModel.DeadLine,
                    Priority = (EPriority)Convert.ToInt32(viewModel.Priority), //!
                    UpdatedAt = timeStamp,
                    CreatedAt = timeStamp,
                };

                await _repository.Create(newItem);

                return new BaseResponse<bool>()
                {
                    Descritption = ResponseMessage.CREATE_SUCCES,
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
                        Descritption = ResponseMessage.NOT_FOUND,
                        StatusCode = EStatusCode.NotFound,
                    };
                }

                itemResponse.DeleteAt = DateTime.UtcNow;
                await _repository.Update(itemResponse);

                return new BaseResponse<bool>()
                {
                    Descritption = ResponseMessage.DELETE_SUCCES,
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
                        Descritption = ResponseMessage.NOT_FOUND,
                        StatusCode = EStatusCode.NotFound,
                    };
                }

                itemResponse.Status = viewModel.Status;
                itemResponse.Name = viewModel.Name;
                //todo.Priority = viewModel.Priority; //!
                itemResponse.Background = viewModel.Background;
                itemResponse.DeadLine = viewModel.DeadLine;
                itemResponse.Category = viewModel.Category;
                itemResponse.UpdatedAt = DateTime.UtcNow;

                var response = await _repository.Update(itemResponse);
                return new BaseResponse<Todo>()
                {
                    Descritption = ResponseMessage.UPDATE_SUCCES,
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
                        Descritption = ResponseMessage.NOT_FOUND,
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
                        Descritption = ResponseMessage.NOT_FOUND,
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
                        Descritption = ResponseMessage.NOT_FOUND,
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
                        Descritption = ResponseMessage.NOT_FOUND,
                        StatusCode = EStatusCode.NotFound,
                    };
                }

                itemResponse.DeleteAt = null;
                await _repository.Update(itemResponse);

                return new BaseResponse<Todo>()
                {
                    Descritption = ResponseMessage.RESTORE_SUCCES,
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
