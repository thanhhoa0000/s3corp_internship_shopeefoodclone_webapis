﻿namespace ShopeeFoodClone.WebApi.Payment.Domain.Interfaces;

public interface IRepository<T> : IDisposable where T : class
{
    Task<IEnumerable<T>> GetAllAsync(
        Expression<Func<T, bool>>? filter = null, 
        Func<IQueryable<T>, IQueryable<T>>? include = null, 
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        bool orderByDescending = false,
        bool tracked = true, 
        int pageSize = 0, 
        int pageNumber = 1);
    Task<T?> GetAsync(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IQueryable<T>>? include = null, bool tracked = true);
    Task CreateAsync(T entity);
    Task RemoveAsync(T entity);
    Task UpdateAsync(T entity);
}
