using System.Collections.Generic;
using System.Threading.Tasks;
using Khabarho.ViewModels;

namespace Khabarho.Services.CategoryService
{
    public interface ICategoryService
    {
        public Task<CategoryViewModel> CreateAsync(CategoryViewModel model);

        public Task<CategoryViewModel> GetAsync(string id);

        public Task<List<CategoryViewModel>> GetAllAsync();

        public Task<CategoryViewModel> UpdateAsync(CategoryViewModel model);

        public Task<bool> DeleteAsync(string id);
    }
}