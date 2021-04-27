using System.Collections.Generic;
using System.Threading.Tasks;
using Khabarho.Models.PostModels;
using Khabarho.ViewModels.PostViewModels;

namespace Khabarho.Services.PostService
{
    public interface IPostService
    {
        public Task<PostViewModel> CreateAsync(PostViewModel model);

        public Task<PostViewModel> GetAsync(string id);

        public Task<List<PostViewModel>> GetAllAsync();

        public Task<PostViewModel> UpdateAsync(PostViewModel model);

        public Task<bool> DeleteAsync(string id);
    }
}