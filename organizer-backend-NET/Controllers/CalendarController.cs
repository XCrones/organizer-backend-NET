using Microsoft.AspNetCore.Mvc;
using organizer_backend_NET.Domain.ViewModel;
using organizer_backend_NET.Interfaces.IControllers;
using organizer_backend_NET.Implements.Interfaces;
using Microsoft.AspNetCore.Authorization;
using organizer_backend_NET.Domain.Enums;

namespace organizer_backend_NET.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class CalendarController : Controller, ICalendar_Controller
    {
        private readonly ICalendarService _calendarService;

        public CalendarController(ICalendarService calendarService)
        {
            _calendarService = calendarService;
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

            } catch (Exception ex)
            {
                return -1;
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CalendarViewModel model)
        {
            int UId = GetUId();

            if (UId != -1)
            {
                var result =  await _calendarService.CreateItem(UId, model);

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
                var result = await _calendarService.GetAll(UId);

                if (result.StatusCode == EStatusCode.OK)
                {
                    return Created("", result.Data);
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
                var result = await _calendarService.GetItemById(UId ,id);

                if (result.StatusCode == EStatusCode.OK)
                {
                    return Created("", result.Data);
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
                var result = await _calendarService.RemoveItem(UId ,id);

                if (result.StatusCode == EStatusCode.OK)
                {
                    return Created("", result.Data);
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
                var result = await _calendarService.RestoreItem(UId ,id);

                if (result.StatusCode == EStatusCode.OK)
                {
                    return Created("", result.Data);
                }

                return BadRequest(result.Description);
            }

            return Unauthorized();
        }

        [Authorize]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Save(int id, CalendarViewModel model)
        {
            int UId = GetUId();

            if (UId != -1)
            {
                var result = await _calendarService.EditItem(UId ,id, model);

                if (result.StatusCode == EStatusCode.OK)
                {
                    return Created("", result.Data);
                }

                return BadRequest(result.Description);
            }

            return Unauthorized();
        }
    }
}
