using Microsoft.AspNetCore.Mvc;
using organizer_backend_NET.Domain.Entity.Todo;
using organizer_backend_NET.Domain.Interfaces.IResponse;
using organizer_backend_NET.Domain.ViewModel.Todo;
using organizer_backend_NET.Service.Interfaces.ITodo;

namespace organizer_backend_NET.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public async Task<IBaseResponse<IEnumerable<Todo>>> Get()
        {
            return await _todoService.GetAll();
        }


        [HttpGet("{id}")]
        public async Task<IBaseResponse<Todo>> GetOne(int id)
        {
            return await _todoService.GetItemById(id);
        }

        [HttpPost]
        public async Task<IBaseResponse<bool>> Create(TodoViewModel model)
        {
            return await _todoService.CreateItem(model);
        }

        [HttpPatch]
        public async Task<IBaseResponse<Todo>> Save(TodoViewModel todo)
        {
            return await _todoService.EditItem(todo.Id, todo);
        }

        [HttpDelete("{id}")]
        public async Task<IBaseResponse<bool>> Remove(int id)
        {
            return await _todoService.DeleteItem(id);
        }

        [HttpPost("restore/{id}")]
        public async Task<IBaseResponse<Todo>> Restore(int id)
        {
            return await _todoService.RestoreItem(id);
        }
    }
}
