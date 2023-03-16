using Microsoft.AspNetCore.Mvc;
using organizer_backend_NET.Service.Interfaces.ITodo;

namespace organizer_backend_NET.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : Controller
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet(Name = "todo")]
        public async Task<string> Get()
        {

            var todos = await _todoService.GetTodos();

            return "test OK";
        }
    }
}
