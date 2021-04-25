using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Khabarho.Db;
using Microsoft.EntityFrameworkCore;
using Khabarho.Extensions;
using Khabarho.Models.PostModels;
using Khabarho.Utilities;

namespace Khabarho.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : Base
    {
        private readonly DataContext _context;
        private readonly DbSet<T> _table;
        
        public BaseRepository(DataContext context)
        {
            _context = context;
            _table = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var result = await _table.Where(x => x.IsDeleted == false).ToListAsync();
            result.CustomNullCheck(ErrorMessages.NotFoundError);

            return result;
        }

        public async Task<T> GetAsync(string id)
        {
            id.CustomNullCheck(ErrorMessages.NullParameterError);
            
            var result = await _table.FirstOrDefaultAsync(x => x.Id.Equals(Guid.Parse(id)) && x.IsDeleted == false);
            
            result.CustomNullCheck(ErrorMessages.NotFoundError);

            return result;
        }

        public async Task<bool> InsertAsync(T entity)
        {
            entity.CustomNullCheck(ErrorMessages.NullParameterError);
            
            await _table.AddAsync(entity);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            entity.CustomNullCheck(ErrorMessages.NullParameterError);
            
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
            entity.CustomNullCheck(ErrorMessages.NullParameterError);

            entity.IsDeleted = true;
            entity.DeletedDate = DateTime.Now;
            entity.Title = "Удалено";
            _context.Update(entity);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }
    }
}