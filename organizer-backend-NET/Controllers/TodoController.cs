using Microsoft.AspNetCore.Mvc;
using organizer_backend_NET.Domain.Entity;
using organizer_backend_NET.Domain.Interfaces;
using organizer_backend_NET.Domain.ViewModel;
using organizer_backend_NET.Interfaces.IControllers;
using organizer_backend_NET.Implements.Interfaces;

namespace organizer_backend_NET.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class TodoController : Controller, ITodo_Controller
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpPost]
        public async Task<IBaseResponse<bool>> Create(TodoViewModel model)
        {
            return await _todoService.CreateItem(model);
        }

        [HttpGet]
        public async Task<IBaseResponse<IEnumerable<Todo>>> GetAll()
        {
            return await _todoService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<IBaseResponse<Todo>> GetOne(int id)
        {
            return await _todoService.GetItemById(id);
        }

        [HttpDelete("{id}")]
        public async Task<IBaseResponse<bool>> Remove(int id)
        {
            return await _todoService.RemoveItem(id);
        }

        [HttpPost("restore/{id}")]
        public async Task<IBaseResponse<Todo>> Restore(int id)
        {
            return await _todoService.RestoreItem(id);
        }

        [HttpPatch("{id}")]
        public async Task<IBaseResponse<Todo>> Save(int id, TodoViewModel model)
        {
            return await _todoService.EditItem(id, model);
        }
    }
}
