using Microsoft.AspNetCore.Mvc;
using organizer_backend_NET.Domain.ViewModel;
using organizer_backend_NET.Implements.Interfaces;
using Microsoft.AspNetCore.Authorization;
using organizer_backend_NET.Domain.Enums;
using organizer_backend_NET.Interfaces.IControllers;

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
        public async Task<IActionResult> Create(TodoViewModel model)
        {
            int UId = GetUId();

            if (UId != -1)
            {
                var result = await _todoService.CreateItem(UId, model);

                if (result.StatusCode == EStatusCode.OK)
                {
                    return Created("", result.Data);
                }

                return BadRequest(result.Description);
            }

            return Unauthorized();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            int UId = GetUId();

            if (UId != -1)
            {
                var result = await _todoService.GetAll(UId);

                if (result.StatusCode == EStatusCode.OK)
                {
                    return Ok(result.Data);
                }

                return BadRequest(result.Description);
            }

            return Unauthorized();
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            int UId = GetUId();

            if (UId != -1)
            {
                var result = await _todoService.GetItemById(UId, id);

                if (result.StatusCode == EStatusCode.OK)
                {
                    return Ok(result.Data);
                }

                if (result.StatusCode == EStatusCode.NotFound)
                {
                    return NotFound(result.Description);
                }

                return BadRequest(result.Description);
            }

            return Unauthorized();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            int UId = GetUId();

            if (UId != -1)
            {
                var result = await _todoService.RemoveItem(UId, id);

                if (result.StatusCode == EStatusCode.OK)
                {
                    return Ok(result.Data);
                }

                if (result.StatusCode == EStatusCode.NotFound)
                {
                    return NotFound(result.Description);
                }

                return BadRequest(result.Description);
            }

            return Unauthorized();
        }

        [Authorize]
        [HttpPost("restore/{id}")]
        public async Task<IActionResult> Restore(int id)
        {
            int UId = GetUId();

            if (UId != -1)
            {
                var result = await _todoService.RestoreItem(UId, id);

                if (result.StatusCode == EStatusCode.OK)
                {
                    return Ok(result.Data);
                }

                if (result.StatusCode == EStatusCode.NotFound)
                {
                    return NotFound(result.Description);
                }

                return BadRequest(result.Description);
            }

            return Unauthorized();
        }

        [Authorize]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Save(int id, TodoViewModel model)
        {
            int UId = GetUId();

            if (UId != -1)
            {
                var result = await _todoService.EditItem(UId, id, model);

                if (result.StatusCode == EStatusCode.OK)
                {
                    return Ok(result.Data);
                }

                if (result.StatusCode == EStatusCode.NotFound)
                {
                    return NotFound(result.Description);
                }

                return BadRequest(result.Description);
            }

            return Unauthorized();
        }
    }
}
