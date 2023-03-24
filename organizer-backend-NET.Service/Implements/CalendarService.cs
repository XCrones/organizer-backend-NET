﻿
using Microsoft.EntityFrameworkCore;
using organizer_backend_NET.DAL.Interfaces;
using organizer_backend_NET.Domain.Entity;
using organizer_backend_NET.Domain.Enums;
using organizer_backend_NET.Domain.Interfaces;
using organizer_backend_NET.Domain.Response;
using organizer_backend_NET.Domain.ViewModel;
using organizer_backend_NET.Implements.Interfaces;

namespace organizer_backend_NET.Implements.Services
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
                    Description = nameof(EMessage.create_succes),
                    StatusCode = EStatusCode.OK,
                };


            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[CreateItem] : {ex.Message}",
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
                        Description = nameof(EMessage.not_found),
                        StatusCode = EStatusCode.NotFound,
                    };
                }

                itemResponse.DeleteAt = DateTime.UtcNow;
                await _repository.Update(itemResponse);

                return new BaseResponse<bool>()
                {
                    Description = nameof(EMessage.delete_succes),
                    StatusCode = EStatusCode.OK,
                    Data = true,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteItem] : {ex.Message}",
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
                        Description = nameof(EMessage.not_found),
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
                    Description =  nameof(EMessage.update_succes),
                    StatusCode = EStatusCode.Edited,
                    Data = response,
                };

            }
            catch (Exception ex)
            {
                return new BaseResponse<Calendar>()
                {
                    Description = $"[DeleteItem] : {ex.Message}",
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
                        Description = nameof(EMessage.not_found),
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
                    Description = $"[GetAll] : {ex.Message}",
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
                        Description = nameof(EMessage.not_found),
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
                    Description = $"[GetItemById] : {ex.Message}",
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
                        Description = nameof(EMessage.not_found),
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
                    Description = $"[GetItemByName] : {ex.Message}",
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
                        Description = nameof(EMessage.not_found),
                        StatusCode = EStatusCode.NotFound,
                    };
                }

                itemResponse.DeleteAt = null;
                await _repository.Update(itemResponse);

                return new BaseResponse<Calendar>()
                {
                    Description = nameof(EMessage.restore_succes),
                    StatusCode = EStatusCode.OK,
                    Data = itemResponse,
                };

            }
            catch (Exception ex)
            {
                return new BaseResponse<Calendar>()
                {
                    Description = $"[RestoreItem] : {ex.Message}",
                    StatusCode = EStatusCode.InternalServerError,
                };
            }
        }
    }
}
