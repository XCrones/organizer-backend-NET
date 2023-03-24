using Microsoft.AspNetCore.Mvc;
using organizer_backend_NET.Domain.Entity;
using organizer_backend_NET.Domain.Interfaces;
using organizer_backend_NET.Domain.ViewModel;
using organizer_backend_NET.Interfaces.IControllers;
using organizer_backend_NET.Implements.Interfaces;
using Microsoft.AspNetCore.Authorization;

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
        public async Task<IBaseResponse<bool>> Create(CalendarViewModel model)
        {
            int UId = GetUId();

            if (UId != -1)
            {
                return await _calendarService.CreateItem(UId, model);
            }

            return (IBaseResponse<bool>)BadRequest("Value must be passed in the request body.");
        }

        [Authorize]
        [HttpGet]
        public async Task<IBaseResponse<IEnumerable<Calendar>>> GetAll()
        {
            int UId = GetUId();

            if (UId != -1)
            {
                return await _calendarService.GetAll(UId);
            }

            return (IBaseResponse<IEnumerable<Calendar>>)BadRequest("Value must be passed in the request body.");
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IBaseResponse<Calendar>> GetOne(int id)
        {
            int UId = GetUId();

            if (UId != -1)
            {
                return await _calendarService.GetItemById(UId ,id);
            }

            return (IBaseResponse<Calendar>)BadRequest("Value must be passed in the request body.");
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IBaseResponse<bool>> Remove(int id)
        {
            int UId = GetUId();

            if (UId != -1)
            {
                return await _calendarService.RemoveItem(UId ,id);
            }

            return (IBaseResponse<bool>)BadRequest("Value must be passed in the request body.");
        }

        [Authorize]
        [HttpPost("restore/{id}")]
        public async Task<IBaseResponse<Calendar>> Restore(int id)
        {
            int UId = GetUId();

            if (UId != -1)
            {
                return await _calendarService.RestoreItem(UId ,id);
            }

            return (IBaseResponse<Calendar>)BadRequest("Value must be passed in the request body.");
        }

        [Authorize]
        [HttpPatch("{id}")]
        public async Task<IBaseResponse<Calendar>> Save(int id, CalendarViewModel model)
        {
            int UId = GetUId();

            if (UId != -1)
            {
                return await _calendarService.EditItem(UId ,id, model);
            }

            return (IBaseResponse<Calendar>)BadRequest("Value must be passed in the request body.");
        }
    }
}
