using organizer_backend_NET.DAL.Interfaces.ITodo;
using organizer_backend_NET.Domain.Enum;
using organizer_backend_NET.Domain.Interfaces.IResponse;
using organizer_backend_NET.Domain.Response.BaseResponse;
using organizer_backend_NET.Domain.ViewModel.Todo;
using organizer_backend_NET.Service.Interfaces.ITodo;
using organizer_backend_NET.Domain.Entity.Todo;
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

        public async Task<IBaseResponse<bool>> CreateTodo(TodoViewModel todoViewModel)
        {
            try
            {
                var newTodo = new Todo()
                {
                    Name = todoViewModel.Name,
                    Background = todoViewModel.Background,
                    Category = todoViewModel.Category,
                    Status = false,
                    Uid = 1, //!
                    DeadLine = todoViewModel.DeadLine,
                    Priority = (TPriority)Convert.ToInt32(todoViewModel.Priority), //!
                };

                await _todoRespository.Create(newTodo);

                return new BaseResponse<bool>() {
                    StatusCode = TStatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Descritption = $"[CreateTodo] : {ex.Message}",
                    StatusCode = TStatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteTodo(int id)
        {
            try
            {
                var todo = await _todoRespository.Read().FirstOrDefaultAsync(x => x.Id == id);

                if (todo == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Descritption = ResponseMessage.NOT_FOUND,
                        StatusCode = TStatusCode.NotFound,
                    };
                }

                await _todoRespository.Delete(todo);

                return new BaseResponse<bool>()
                {
                    StatusCode = TStatusCode.OK,
                };

            } catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Descritption = $"[DeleteTodo] : {ex.Message}",
                    StatusCode = TStatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<Domain.Entity.Todo.Todo>> EditTodo(int id, TodoViewModel todoViewModel)
        {
            try
            {
                var todo = await _todoRespository.Read().FirstOrDefaultAsync(x => x.Id == id);

                if (todo == null)
                {
                    return new BaseResponse<Todo>()
                    {
                        Descritption = ResponseMessage.NOT_FOUND,
                        StatusCode = TStatusCode.NotFound,
                    };
                }

                todo.Status = todoViewModel.Status;
                todo.Name = todoViewModel.Name;
                //todo.Priority = todoViewModel.Priority; //!
                todo.Background = todoViewModel.Background;
                todo.DeadLine = todoViewModel.DeadLine;
                todo.Category = todoViewModel.Category;
                todo.Uid = 1; //! 

                var response = await _todoRespository.Update(todo);
                return new BaseResponse<Todo>()
                {
                    StatusCode = TStatusCode.Edited,
                    Data = response,
                };
            } catch (Exception ex)
            {
                return new BaseResponse<Todo>()
                {
                    Descritption = $"[EditTodo] : {ex.Message}",
                    StatusCode = TStatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<Domain.Entity.Todo.Todo>> GetTodoById(int id)
        {
            try
            {
                var todo = await _todoRespository.Read().FirstOrDefaultAsync(todo => todo.Id == id);

                if (todo == null)
                {
                    return new BaseResponse<Todo>()
                    {
                        Descritption = ResponseMessage.NOT_FOUND,
                        StatusCode = TStatusCode.NotFound,
                    };
                }

                return new BaseResponse<Todo>()
                {
                    StatusCode = TStatusCode.OK,
                    Data = todo,
                };

            } catch (Exception ex)
            {
                return new BaseResponse<Todo>()
                {
                    Descritption = $"[GetTodoById] : {ex.Message}",
                    StatusCode = TStatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<Domain.Entity.Todo.Todo>> GetTodoByName(string name)
        {
            try
            {
                var todo = await _todoRespository.Read().FirstOrDefaultAsync(todo => todo.Name == name);

                if (todo == null)
                {
                    return new BaseResponse<Todo>()
                    {
                        Descritption = ResponseMessage.NOT_FOUND,
                        StatusCode = TStatusCode.NotFound,
                    };
                }

                return new BaseResponse<Todo>()
                {
                    StatusCode = TStatusCode.OK,
                    Data = todo,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Todo>()
                {
                    Descritption = $"[GetTodoByName] : {ex.Message}",
                    StatusCode = TStatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<Domain.Entity.Todo.Todo>>> GetTodos()
        {
            try
            {
                var todos = await _todoRespository.Read().ToListAsync();

                if (todos == null)
                {
                    return new BaseResponse<IEnumerable<Todo>>()
                    {
                        Descritption = ResponseMessage.NOT_FOUND,
                        StatusCode = TStatusCode.NotFound,
                    };
                }

                return new BaseResponse<IEnumerable<Todo>>()
                {
                    StatusCode = TStatusCode.OK,
                    Data = todos,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Todo>>()
                {
                    Descritption = $"[GetTodos] : {ex.Message}",
                    StatusCode = TStatusCode.InternalServerError,
                };
            }
        }
    }
}
