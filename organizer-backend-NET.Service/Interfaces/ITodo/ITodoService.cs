using organizer_backend_NET.Domain.Entity.Todo;
using organizer_backend_NET.Domain.Interfaces.IResponse;
using organizer_backend_NET.Domain.ViewModel.Todo;

namespace organizer_backend_NET.Service.Interfaces.ITodo
{
    public interface ITodoService
    {
        Task<IBaseResponse<IEnumerable<Todo>>> GetTodos();

        Task<IBaseResponse<Todo>> GetTodoById(int id);

        Task<IBaseResponse<Todo>> GetTodoByName(string name);

        Task<IBaseResponse<bool>> DeleteTodo(int id);

        Task<IBaseResponse<bool>> CreateTodo(TodoViewModel todoViewModel);

        Task<IBaseResponse<Todo>> EditTodo(int id, TodoViewModel todoViewModel);
    }
}
