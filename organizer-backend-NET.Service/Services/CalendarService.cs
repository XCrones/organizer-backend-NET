
using Microsoft.EntityFrameworkCore;
using organizer_backend_NET.DAL.Interfaces.ICalendar;
using organizer_backend_NET.Domain.Common;
using organizer_backend_NET.Domain.Entity;
using organizer_backend_NET.Domain.Enum;
using organizer_backend_NET.Domain.Interfaces.IResponse;
using organizer_backend_NET.Domain.Response.BaseResponse;
using organizer_backend_NET.Domain.ViewModel.Calendar;
using organizer_backend_NET.Service.Interfaces;

namespace organizer_backend_NET.Service.Services
{
    public class CalendarService : ICalendarService
    {
        private readonly ICalendarRepository _repository;

        public CalendarService(ICalendarRepository calendarRepository)
        {
            _repository = calendarRepository;
        }

        public async Task<IBaseResponse<bool>> CreateItem(CalendarViewModel viewModel)
        {
            try
            {
                DateTime timeStamp = DateTime.UtcNow;

                var newEvent = new Calendar()
                {
                    Background = viewModel.Background,
                    Name = viewModel.Name,
                    EventStart = viewModel.EventStart,
                    EventEnd = viewModel.EventEnd,
                    Description = viewModel.Description,
                    UId = 1, //!
                    CreatedAt = timeStamp,
                    UpdatedAt = timeStamp,
                };

                await _repository.Create(newEvent);

                return new BaseResponse<bool>()
                {
                    Descritption = ResponseMessage.CREATE_SUCCES,
                    StatusCode = EStatusCode.OK,
                };


            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Descritption = $"[CreateItem] : {ex.Message}",
                    StatusCode = EStatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<bool>> RemoveItem(int id)
        {
            try
            {
                var itemResponse = await _repository.Read().FirstOrDefaultAsync(item => item.Id == id && item.DeleteAt == null);

                if (itemResponse == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Descritption = ResponseMessage.NOT_FOUND,
                        StatusCode = EStatusCode.NotFound,
                    };
                }

                itemResponse.DeleteAt = DateTime.UtcNow;
                await _repository.Update(itemResponse);

                return new BaseResponse<bool>()
                {
                    Descritption = ResponseMessage.DELETE_SUCCES,
                    StatusCode = EStatusCode.OK,
                    Data = true,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Descritption = $"[DeleteItem] : {ex.Message}",
                    StatusCode = EStatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<Calendar>> EditItem(int id, CalendarViewModel viewModel)
        {
            try
            {

                var itemResponse = await _repository.Read().FirstOrDefaultAsync(item => item.Id == id && item.DeleteAt == null);

                if (itemResponse == null)
                {
                    return new BaseResponse<Calendar>()
                    {
                        Descritption = ResponseMessage.NOT_FOUND,
                        StatusCode = EStatusCode.NotFound,
                    };
                }


                itemResponse.Name = viewModel.Name;
                itemResponse.Description = viewModel.Description;
                itemResponse.Background = viewModel.Background;
                itemResponse.UpdatedAt = DateTime.UtcNow;
                itemResponse.EventEnd = viewModel.EventEnd;
                itemResponse.EventStart = viewModel.EventStart;

                var response = await _repository.Update(itemResponse);
                return new BaseResponse<Calendar>()
                {
                    Descritption = ResponseMessage.UPDATE_SUCCES,
                    StatusCode = EStatusCode.Edited,
                    Data = response,
                };

            }
            catch (Exception ex)
            {
                return new BaseResponse<Calendar>()
                {
                    Descritption = $"[DeleteItem] : {ex.Message}",
                    StatusCode = EStatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<Calendar>>> GetAll()
        {
            try
            {
                var itemsResponse = await _repository.Read().Where(item => item.DeleteAt == null).ToListAsync();

                if (itemsResponse == null)
                {
                    return new BaseResponse<IEnumerable<Calendar>>()
                    {
                        Descritption = ResponseMessage.NOT_FOUND,
                        StatusCode = EStatusCode.NotFound,
                    };
                }

                return new BaseResponse<IEnumerable<Calendar>>()
                {
                    StatusCode = EStatusCode.OK,
                    Data = itemsResponse,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Calendar>>()
                {
                    Descritption = $"[GetAll] : {ex.Message}",
                    StatusCode = EStatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<Calendar>> GetItemById(int id)
        {
            try
            {
                var itemResponse = await _repository.Read().FirstOrDefaultAsync(item => item.Id == id && item.DeleteAt == null);

                if (itemResponse == null)
                {
                    return new BaseResponse<Calendar>()
                    {
                        Descritption = ResponseMessage.NOT_FOUND,
                        StatusCode = EStatusCode.NotFound,
                    };
                }

                return new BaseResponse<Calendar>()
                {
                    StatusCode = EStatusCode.OK,
                    Data = itemResponse,
                };

            }
            catch (Exception ex)
            {
                return new BaseResponse<Calendar>()
                {
                    Descritption = $"[GetItemById] : {ex.Message}",
                    StatusCode = EStatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<Calendar>> GetItemByName(string name)
        {
            try
            {
                var itemResponse = await _repository.Read().FirstOrDefaultAsync(item => item.Name == name && item.DeleteAt == null);

                if (itemResponse == null)
                {
                    return new BaseResponse<Calendar>()
                    {
                        Descritption = ResponseMessage.NOT_FOUND,
                        StatusCode = EStatusCode.NotFound,
                    };
                }

                return new BaseResponse<Calendar>()
                {
                    StatusCode = EStatusCode.OK,
                    Data = itemResponse,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Calendar>()
                {
                    Descritption = $"[GetItemByName] : {ex.Message}",
                    StatusCode = EStatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<Calendar>> RestoreItem(int id)
        {
            try
            {
                var itemResponse = await _repository.Read().FirstOrDefaultAsync(item => item.Id == id && item.DeleteAt != null);

                if (itemResponse == null)
                {
                    return new BaseResponse<Calendar>()
                    {
                        Descritption = ResponseMessage.NOT_FOUND,
                        StatusCode = EStatusCode.NotFound,
                    };
                }

                itemResponse.DeleteAt = null;
                await _repository.Update(itemResponse);

                return new BaseResponse<Calendar>()
                {
                    Descritption = ResponseMessage.RESTORE_SUCCES,
                    StatusCode = EStatusCode.OK,
                    Data = itemResponse,
                };

            }
            catch (Exception ex)
            {
                return new BaseResponse<Calendar>()
                {
                    Descritption = $"[RestoreItem] : {ex.Message}",
                    StatusCode = EStatusCode.InternalServerError,
                };
            }
        }
    }
}
