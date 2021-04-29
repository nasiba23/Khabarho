using System.Collections.Generic;
using System.Threading.Tasks;
using Khabarho.ViewModels;

namespace Khabarho.Services.LikeService
{
    public interface ILikeService
    {
        public Task<LikeViewModel> CreateAsync(LikeViewModel model);

        public Task<LikeViewModel> GetAsync(string id);

        public Task<List<LikeViewModel>> GetAllAsync();
        
        public Task<bool> DeleteAsync(LikeViewModel model);

        public bool LikeCheck(LikeViewModel model);
    }
}