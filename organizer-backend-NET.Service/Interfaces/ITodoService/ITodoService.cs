using organizer_backend_NET.Domain.Entity;
using organizer_backend_NET.Domain.ViewModel.Todo;
using organizer_backend_NET.Service.Interfaces.IBaseService;

namespace organizer_backend_NET.Service.Interfaces.ITodo
{
    public interface ITodoService : IBaseService<Todo, TodoViewModel>
    {
    }
}
