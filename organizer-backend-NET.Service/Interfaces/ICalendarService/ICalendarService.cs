
using organizer_backend_NET.Domain.Entity.Calendar;
using organizer_backend_NET.Domain.ViewModel.Calendar;
using organizer_backend_NET.Service.Interfaces.IBaseService;

namespace organizer_backend_NET.Service.Interfaces.ICalendarService
{
    public interface ICalendarService: IBaseService<Calendar, CalendarViewModel>
    {
    }
}
