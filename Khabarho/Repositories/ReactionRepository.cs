using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Khabarho.Db;
using Khabarho.Extensions;
using Khabarho.Models;
using Khabarho.Models.PostModels;
using Khabarho.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Khabarho.Repositories
{
    public class ReactionRepository<T> : IReactionRepository<T> where T : BaseReaction
    {
        private readonly DataContext _context;
        private readonly DbSet<T> _table;

        public ReactionRepository(DataContext context)
        {
            _context = context;
            _table = context.Set<T>();
        }
        
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var result = await _table.ToListAsync();
            
            result.CustomNullCheck(ErrorMessages.NullParameterError);
            
            return result;
        }

        public async Task<T> GetAsync(string id)
        {
            id.CustomNullCheck(ErrorMessages.NullParameterError);
            
            var result = await _table.FirstOrDefaultAsync(x => x.Id.Equals(Guid.Parse(id)));
            
            result.CustomNullCheck(ErrorMessages.NullParameterError);

            return result;
        }

        public async Task<bool> InsertAsync(T entity)
        {
            entity.CustomNullCheck(ErrorMessages.NullParameterError);

            await _table.AddAsync(entity);
            
            var result = await _context.SaveChangesAsync();
            
            return result > 0;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            entity.CustomNullCheck(ErrorMessages.NullParameterError);

            _table.Remove(entity);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }
    }
}