using organizer_backend_NET.DAL.Interfaces.ITodo;
using organizer_backend_NET.Domain.Interfaces.IResponse;
using organizer_backend_NET.Domain.ViewModel.Todo;
using organizer_backend_NET.Service.Interfaces.ITodo;
using organizer_backend_NET.Domain.Entity.Todo;
using organizer_backend_NET.Domain.Enum;
using organizer_backend_NET.Domain.Response.BaseResponse;
using Microsoft.EntityFrameworkCore;
using organizer_backend_NET.Domain.Common;

namespace organizer_backend_NET.Service.Implementations.TodoService
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRespository _todoRespository;

        public TodoService(ITodoRespository todoRespository)
        {
            _todoRespository = todoRespository;
        }

        public async Task<IBaseResponse<bool>> CreateItem(TodoViewModel viewModel)
        {
            try
            {
                DateTime timeStamp = DateTime.UtcNow;

                var newTodo = new Todo()
                {
                    Name = viewModel.Name,
                    Background = viewModel.Background,
                    Category = viewModel.Category,
                    Status = false,
                    Uid = 1, //!
                    DeadLine = viewModel.DeadLine,
                    Priority = (EPriority)Convert.ToInt32(viewModel.Priority), //!
                    UpdatedAt = timeStamp,
                    CreatedAt = timeStamp,
                };

                await _todoRespository.Create(newTodo);

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

        public async Task<IBaseResponse<bool>> DeleteItem(int id)
        {
            try
            {
                var todo = await _todoRespository.Read().FirstOrDefaultAsync(x => x.Id == id && x.DeleteAt == null);

                if (todo == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Descritption = ResponseMessage.NOT_FOUND,
                        StatusCode = EStatusCode.NotFound,
                    };
                }

                todo.DeleteAt = DateTime.UtcNow;
                await _todoRespository.Update(todo);

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
                var todo = await _todoRespository.Read().FirstOrDefaultAsync(x => x.Id == id && x.DeleteAt == null);

                if (todo == null)
                {
                    return new BaseResponse<Todo>()
                    {
                        Descritption = ResponseMessage.NOT_FOUND,
                        StatusCode = EStatusCode.NotFound,
                    };
                }

                todo.Status = viewModel.Status;
                todo.Name = viewModel.Name;
                //todo.Priority = viewModel.Priority; //!
                todo.Background = viewModel.Background;
                todo.DeadLine = viewModel.DeadLine;
                todo.Category = viewModel.Category;
                todo.Uid = 1; //! 
                todo.UpdatedAt = DateTime.UtcNow;

                var response = await _todoRespository.Update(todo);
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
                var todos = await _todoRespository.Read().Where(x => x.DeleteAt == null).ToListAsync();

                if (todos == null)
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
                    Data = todos,
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
                var todo = await _todoRespository.Read().FirstOrDefaultAsync(x => x.Id == id && x.DeleteAt == null);

                if (todo == null)
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
                    Data = todo,
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
                var todo = await _todoRespository.Read().FirstOrDefaultAsync(x => x.Name == name && x.DeleteAt == null);

                if (todo == null)
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
                    Data = todo,
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
                var todo = await _todoRespository.Read().FirstOrDefaultAsync(x => x.Id == id && x.DeleteAt != null);

                if (todo == null)
                {
                    return new BaseResponse<Todo>()
                    {
                        Descritption = ResponseMessage.NOT_FOUND,
                        StatusCode = EStatusCode.NotFound,
                    };
                }

                todo.DeleteAt = null;
                await _todoRespository.Update(todo);

                return new BaseResponse<Todo>()
                {
                    Descritption = ResponseMessage.RESTORE_SUCCES,
                    StatusCode = EStatusCode.OK,
                    Data= todo,
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
