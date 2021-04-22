using System.Threading.Tasks;
using Khabarho.Models;

namespace Khabarho.Repositories
{
    public interface IUpdatableReactionRepository<T> where T : BaseReaction
    {
        Task<bool> UpdateAsync(T entity);
    }
}