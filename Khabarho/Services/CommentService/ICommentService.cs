using System.Collections.Generic;
using System.Threading.Tasks;
using Khabarho.ViewModels;

namespace Khabarho.Services.CommentService
{
    public interface ICommentService
    {
        public Task<CommentViewModel> CreateAsync(CommentViewModel model);

        public Task<CommentViewModel> GetAsync(string id);

        public Task<List<CommentViewModel>> GetAllAsync();

        public Task<CommentViewModel> UpdateAsync(CommentViewModel model);

        public Task<bool> DeleteAsync(string id);
    }
}