using System.Collections.Generic;
using System.Threading.Tasks;
using Khabarho.Models;
using Khabarho.Models.PostModels;

namespace Khabarho.Repositories
{
    public interface IReactionRepository<T> where T : BaseReaction
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> Get(string id);
        Task<bool> InsertAsync(T entity);
        Task<bool> DeleteAsync(T entity);
    }
}