using System;
using System.Threading.Tasks;
using Khabarho.Db;
using Khabarho.Models;
using Microsoft.EntityFrameworkCore;

namespace Khabarho.Repositories
{
    public class UpdatableReactionRepository<T> : ReactionRepository<T>, IUpdatableReactionRepository<T> where T : BaseReaction
    {
        private readonly DataContext _context;
        private readonly DbSet<T> _table;
        
        private const string NullParameterError = "Упс, что-то пошло не так. Попробуйте снова :)";

        
        public UpdatableReactionRepository(DataContext context) : base(context)
        {
            _context = context;
            _table = context.Set<T>();
        }
        
        public async Task<bool> UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(null, NullParameterError);
            }
            
            _table.Update(entity);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }
    }
}