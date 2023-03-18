using Microsoft.AspNetCore.Mvc;
using organizer_backend_NET.Domain.Entity;
using organizer_backend_NET.Domain.Interfaces;
using organizer_backend_NET.Domain.ViewModel;
using organizer_backend_NET.Interfaces.IControllers;
using organizer_backend_NET.Service.Interfaces;

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

        [HttpPost]
        public async Task<IBaseResponse<bool>> Create(CalendarViewModel model)
        {
            return await _calendarService.CreateItem(model);
        }

        [HttpGet]
        public async Task<IBaseResponse<IEnumerable<Calendar>>> GetAll()
        {
            return await _calendarService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<IBaseResponse<Calendar>> GetOne(int id)
        {
            return await _calendarService.GetItemById(id);
        }

        [HttpDelete("{id}")]
        public async Task<IBaseResponse<bool>> Remove(int id)
        {
            return await _calendarService.RemoveItem(id);
        }

        [HttpPost("restore/{id}")]
        public async Task<IBaseResponse<Calendar>> Restore(int id)
        {
            return await _calendarService.RestoreItem(id);
        }

        [HttpPatch]
        public async Task<IBaseResponse<Calendar>> Save(CalendarViewModel todo)
        {
            return await _calendarService.EditItem(todo.Id, todo);
        }
    }
}
