using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Khabarho.ViewModels;

namespace Khabarho.Services.TypeService
{
    public interface ITypeService
    {
        public Task<TypeViewModel> CreateAsync(TypeViewModel model);

        public Task<TypeViewModel> GetAsync(string id);

        public Task<List<TypeViewModel>> GetAllAsync();

        public Task<TypeViewModel> UpdateAsync(TypeViewModel model);

        public Task<bool> DeleteAsync(string id);
    }
}