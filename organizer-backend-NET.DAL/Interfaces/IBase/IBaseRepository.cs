﻿namespace organizer_backend_NET.DAL.Interfaces.IBase
{
    public interface IBaseRepository<T>
    {
        Task<bool> Create(T entity);

        IQueryable<T> Read();

        Task<T> Update(T entity);

        Task<bool> Delete(T entity);
    }
}
