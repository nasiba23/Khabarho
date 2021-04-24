using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Khabarho.Db;
using Khabarho.Models;
using Khabarho.Models.PostModels;
using Microsoft.EntityFrameworkCore;

namespace Khabarho.Repositories
{
    public class ReactionRepository<T> : IReactionRepository<T> where T : BaseReaction
    {
        private readonly DataContext _context;
        private readonly DbSet<T> _table;
        
        private const string NotFoundError = "Упс, что-то пошло не так. Попробуйте снова :)";
        private const string NullParameterError = "Упс, что-то пошло не так. Попробуйте снова :)";

        public ReactionRepository(DataContext context)
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

        public async Task<bool> DeleteAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(null, NullParameterError);
            }

            _table.Remove(entity);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }
    }
}