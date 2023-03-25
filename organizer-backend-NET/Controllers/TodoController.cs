using Microsoft.AspNetCore.Mvc;
using organizer_backend_NET.Domain.ViewModel;
using organizer_backend_NET.Implements.Interfaces;
using Microsoft.AspNetCore.Authorization;
using organizer_backend_NET.Interfaces.IControllers;
using organizer_backend_NET.Response;
using organizer_backend_NET.Domain.Entity;
using System.Net;

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

                if (result.StatusCode == HttpStatusCode.Created)
                {
                    return Created("", new ActionResponse<Todo>
                    {
                        Message = result.Description,
                        Code = result.StatusCode,
                        Data = result.Data,
                    });
                }

                return BadRequest(new ActionResponse<Todo>
                {
                    Message = result.Description,
                    Code = result.StatusCode,
                });
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

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return Ok(new ActionResponse<IEnumerable<Todo>>
                    {
                        Message = result.Description,
                        Code = result.StatusCode,
                        Data = result.Data,
                    });
                }

                return BadRequest(new ActionResponse<Todo>
                {
                    Message = result.Description,
                    Code = result.StatusCode,
                });
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

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return Ok(new ActionResponse<Todo>
                    {
                        Message = result.Description,
                        Code = result.StatusCode,
                        Data = result.Data,
                    });
                }

                if (result.StatusCode == HttpStatusCode.NotFound)
                {
                    return NotFound(new ActionResponse<Todo>
                    {
                        Message = result.Description,
                        Code = result.StatusCode,
                    });
                }

                return BadRequest(new ActionResponse<Todo>
                {
                    Message = result.Description,
                    Code = result.StatusCode,
                });
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

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return Ok(new ActionResponse<bool>
                    {
                        Message = result.Description,
                        Code = result.StatusCode,
                        Data = result.Data,
                    });
                }

                if (result.StatusCode == HttpStatusCode.NotFound)
                {
                    return NotFound(new ActionResponse<Todo>
                    {
                        Message = result.Description,
                        Code = result.StatusCode,
                    });
                }

                return BadRequest(new ActionResponse<bool>
                {
                    Message = result.Description,
                    Code = result.StatusCode,
                });
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

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return Ok(new ActionResponse<Todo>
                    {
                        Message = result.Description,
                        Code = result.StatusCode,
                        Data = result.Data,
                    });
                }

                if (result.StatusCode == HttpStatusCode.NotFound)
                {
                    return NotFound(new ActionResponse<Todo>
                    {
                        Message = result.Description,
                        Code = result.StatusCode,
                    });
                }

                return BadRequest(new ActionResponse<Todo>
                {
                    Message = result.Description,
                    Code = result.StatusCode,
                });
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

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return Ok(new ActionResponse<Todo>
                    {
                        Message = result.Description,
                        Code = result.StatusCode,
                        Data = result.Data,
                    });
                }

                if (result.StatusCode == HttpStatusCode.NotFound)
                {
                    return NotFound(new ActionResponse<User>
                    {
                        Message = result.Description,
                        Code = result.StatusCode,
                    });
                }

                return BadRequest(new ActionResponse<Todo>
                {
                    Message = result.Description,
                    Code = result.StatusCode,
                });
            }

            return Unauthorized();
        }
    }
}
