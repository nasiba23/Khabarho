using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Khabarho.Db;
using Khabarho.Models;
using Microsoft.EntityFrameworkCore;

namespace Khabarho.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : Base
    {
        private readonly DataContext _context;
        private readonly DbSet<T> _table;

        private const string NotFoundError = "Упс, что-то пошло не так. Попробуйте снова :)";
        private const string NullParameterError = "Упс, что-то пошло не так. Попробуйте снова :)";

        public BaseRepository(DataContext context)
        {
            _context = context;
            _table = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var result = await _table.ToListAsync();

            if (!result.Any())
            {
                throw new ArgumentNullException(null, NotFoundError);
            }
            
            return result;
        }

        public async Task<T> Get(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(null, NullParameterError);
            }
            
            var result = await _table.FirstOrDefaultAsync(x => x.Id.Equals(Guid.Parse(id)));

            if (result == null)
            {
                throw new ArgumentNullException(null, NotFoundError);
            }

            return result;
        }

        public async Task<bool> InsertAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(null, NullParameterError);
            }

            await _table.AddAsync(entity);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(null, NullParameterError);
            }
            
            entity.UpdatedDate = DateTime.Now;
            _table.Update(entity);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }
        
        /// <summary>
        /// Instead of deleting updates isDeleted field in entity
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>succeeded or not</returns>
        /// <exception cref="ArgumentNullException">Null parameter</exception>
        public async Task<bool> DeleteAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(null, NullParameterError);
            }

            entity.IsDeleted = true;
            entity.DeletedDate = DateTime.Now;
            entity.Title = "Удалено";
            _context.Update(entity);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }
    }
}