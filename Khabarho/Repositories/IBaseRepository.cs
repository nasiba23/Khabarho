using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Khabarho.Models;

namespace Khabarho.Repositories
{
    public interface IBaseRepository<T> where T : Base
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> Get(string id);
        Task<bool> InsertAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
    }
}