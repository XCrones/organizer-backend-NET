﻿

using organizer_backend_NET.Domain.Interfaces.IResponse;

namespace organizer_backend_NET.Service.Interfaces.IBaseService
{
    public interface IBaseService<TD, VM>
    {
        Task<IBaseResponse<IEnumerable<TD>>> GetAll();

        Task<IBaseResponse<TD>> GetItemById(int id);

        Task<IBaseResponse<TD>> GetItemByName(string name);

        Task<IBaseResponse<bool>> DeleteItem(int id);

        Task<IBaseResponse<bool>> CreateItem(VM viewModel);

        Task<IBaseResponse<TD>> EditItem(int id, VM viewModel);

        Task<IBaseResponse<TD>> RestoreItem(int id);
    }
}