using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Khabarho.Models.PostModels;
using Khabarho.Repositories;
using Khabarho.ViewModels.CategoryViewModels;
using Khabarho.ViewModels.PostViewModels;
using Microsoft.Extensions.Logging;

namespace Khabarho.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private IBaseRepository<Category> _repo;
        private IMapper _mapper;
        private ILogger<CategoryService> _logger;

        public CategoryService(IBaseRepository<Category> repo, IMapper mapper, ILogger<CategoryService> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }
        
        public async Task<CategoryViewModel> CreateAsync(CategoryViewModel model)
        {
            var categoryViewModel = new CategoryViewModel();
            
            try
            {
                model.CreatedDate = DateTime.Now;
                var category = _mapper.Map<Category>(model);
                var result = await _repo.InsertAsync(category);
                
                if (!result)
                {
                    return new CategoryViewModel();
                }
                
                categoryViewModel = _mapper.Map<CategoryViewModel>(category);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
            }

            return categoryViewModel;
        }

        public async Task<CategoryViewModel> GetAsync(string id)
        {
            var categoryViewModel = new CategoryViewModel();
            
            try
            { 
                var category = await _repo.GetAsync(id);
                categoryViewModel = _mapper.Map<CategoryViewModel>(category);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
            }

            return categoryViewModel;
        }

        public async Task<List<CategoryViewModel>> GetAllAsync()
        {
            var categoriesViewModel = new List<CategoryViewModel>();
            
            try
            {
                var categories = await _repo.GetAllAsync();
                categoriesViewModel = categories.Select(c=> _mapper.Map<CategoryViewModel>(c)).ToList();
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
            }

            return categoriesViewModel;
        }

        public async Task<CategoryViewModel> UpdateAsync(CategoryViewModel model)
        {
            var categoryViewModel = new CategoryViewModel();
            
            try
            {
                var category = _mapper.Map<Category>(model);
                var result = await _repo.UpdateAsync(category);
                
                if (!result)
                {
                    return new CategoryViewModel();
                }
                
                categoryViewModel = _mapper.Map<CategoryViewModel>(category);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
            }

            return categoryViewModel;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = false;
            try
            {
                var category = await _repo.GetAsync(id);
                result = await _repo.DeleteAsync(category);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
            }

            return result;
        }
    }
}