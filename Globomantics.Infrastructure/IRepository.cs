﻿namespace Globomantics.Infrastructure
{
    public interface IRepository<T>
    {
        Task<T> GetAsync(Guid Id);

        Task<T> FindByAsync(string value);
        Task<IEnumerable<T>> AllAsync();
        Task AddAsync(T item);
        Task SaveChangesAsync();
    }
}