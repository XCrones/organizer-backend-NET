using Microsoft.AspNetCore.Mvc;
using organizer_backend_NET.Domain.Entity;
using organizer_backend_NET.Domain.Interfaces;
using organizer_backend_NET.Domain.ViewModel;
using organizer_backend_NET.Interfaces.IControllers;
using organizer_backend_NET.Implements.Interfaces;
using Microsoft.AspNetCore.Authorization;
using organizer_backend_NET.Implements.Services;

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

        private int GetUId()
        {
            try
            {
                var UId = User.Claims.Where(a => a.Type == "UId").FirstOrDefault().Value;

                if (UId == null || string.IsNullOrWhiteSpace(UId))
                {
                    return -1;
                }

                return Int32.Parse(UId);

            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IBaseResponse<bool>> Create(TodoViewModel model)
        {
            int UId = GetUId();

            if (UId != -1)
            {
                return await _todoService.CreateItem(UId, model);
            }

            return (IBaseResponse<bool>)BadRequest("Value must be passed in the request body.");
        }

        [Authorize]
        [HttpGet]
        public async Task<IBaseResponse<IEnumerable<Todo>>> GetAll()
        {
            int UId = GetUId();

            if (UId != -1)
            {
                return await _todoService.GetAll(UId);
            }

            return (IBaseResponse<IEnumerable<Todo>>)BadRequest("Value must be passed in the request body.");
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IBaseResponse<Todo>> GetOne(int id)
        {
            int UId = GetUId();

            if (UId != -1)
            {
                return await _todoService.GetItemById(UId, id);
            }

            return (IBaseResponse<Todo>)BadRequest("Value must be passed in the request body.");
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IBaseResponse<bool>> Remove(int id)
        {
            int UId = GetUId();

            if (UId != -1)
            {
                return await _todoService.RemoveItem(UId, id);
            }

            return (IBaseResponse<bool>)BadRequest("Value must be passed in the request body.");
        }

        [Authorize]
        [HttpPost("restore/{id}")]
        public async Task<IBaseResponse<Todo>> Restore(int id)
        {
            int UId = GetUId();

            if (UId != -1)
            {
                return await _todoService.RestoreItem(UId, id);
            }

            return (IBaseResponse<Todo>)BadRequest("Value must be passed in the request body.");
        }

        [Authorize]
        [HttpPatch("{id}")]
        public async Task<IBaseResponse<Todo>> Save(int id, TodoViewModel model)
        {
            int UId = GetUId();

            if (UId != -1)
            {
                return await _todoService.EditItem(UId, id, model);
            }

            return (IBaseResponse<Todo>)BadRequest("Value must be passed in the request body.");
        }
    }
}
