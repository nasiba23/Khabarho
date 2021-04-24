using System.Threading.Tasks;
using Khabarho.Models;
using Khabarho.Models.PostModels;

namespace Khabarho.Repositories
{
    public interface IUpdatableReactionRepository<T> where T : BaseReaction
    {
        Task<bool> UpdateAsync(T entity);
    }
}