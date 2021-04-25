using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Khabarho.Repositories;
using Khabarho.ViewModels.TypeViewModels;
using Microsoft.Extensions.Logging;
using Type = Khabarho.Models.PostModels.Type;

namespace Khabarho.Services.TypeService
{
    public class TypeService : ITypeService
    {
        private IBaseRepository<Type> _repo;
        private IMapper _mapper;
        private ILogger<TypeService> _logger;

        public TypeService(IBaseRepository<Type> repo, IMapper mapper, ILogger<TypeService> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }
        
        public async Task<TypeViewModel> CreateAsync(TypeViewModel model)
        {
            var typeViewModel = new TypeViewModel();
            
            try
            {
                model.CreatedDate = DateTime.UtcNow;
                var type = _mapper.Map<Type>(model);
                var result = await _repo.InsertAsync(type);
                
                if (!result)
                {
                    return new TypeViewModel();
                }
                
                typeViewModel = _mapper.Map<TypeViewModel>(type);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
            }

            return typeViewModel;
        }

        public async Task<TypeViewModel> GetAsync(string id)
        {
            var typeViewModel = new TypeViewModel();
            
            try
            { 
                var type = await _repo.GetAsync(id);
                typeViewModel = _mapper.Map<TypeViewModel>(type);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
            }

            return typeViewModel;
        }

        public async Task<List<TypeViewModel>> GetAllAsync()
        {
            var typeViewModel = new List<TypeViewModel>();
            
            try
            {
                var categories = await _repo.GetAllAsync();
                typeViewModel = categories.Select(c=> _mapper.Map<TypeViewModel>(c)).ToList();
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
            }

            return typeViewModel;
        }

        public async Task<TypeViewModel> UpdateAsync(TypeViewModel model)
        {
            var typeViewModel = new TypeViewModel();
            
            try
            {
                var type = _mapper.Map<Type>(model);
                var result = await _repo.UpdateAsync(type);
                
                if (!result)
                {
                    return new TypeViewModel();
                }
                
                typeViewModel = _mapper.Map<TypeViewModel>(type);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
            }

            return typeViewModel;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = false;
            try
            {
                var type = await _repo.GetAsync(id);
                result = await _repo.DeleteAsync(type);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
            }

            return result;
        }
    }
}