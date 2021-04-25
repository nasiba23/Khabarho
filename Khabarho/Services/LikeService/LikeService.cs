using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Khabarho.Models.PostModels;
using Khabarho.Repositories;
using Khabarho.ViewModels.LikeViewModels;
using Microsoft.Extensions.Logging;
using Type = Khabarho.Models.PostModels.Type;

namespace Khabarho.Services.LikeService
{
    public class LikeService : ILikeService
    {
        private IReactionRepository<Like> _repo;
        private IMapper _mapper;
        private ILogger<LikeService> _logger;

        public LikeService(IReactionRepository<Like> repo, IMapper mapper, ILogger<LikeService> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }
        
        public async Task<LikeViewModel> CreateAsync(LikeViewModel model)
        {
            var likeViewModel = new LikeViewModel();
            
            try
            {
                model.CreatedDate = DateTime.Now;
                var like = _mapper.Map<Like>(model);
                var result = await _repo.InsertAsync(like);
                
                if (!result)
                {
                    return new LikeViewModel();
                }
                
                likeViewModel = _mapper.Map<LikeViewModel>(like);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
            }

            return likeViewModel;
        }

        public async Task<LikeViewModel> GetAsync(string id)
        {
            var likeViewModel = new LikeViewModel();
            
            try
            { 
                var like = await _repo.GetAsync(id);
                likeViewModel = _mapper.Map<LikeViewModel>(like);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
            }

            return likeViewModel;
        }

        public async Task<List<LikeViewModel>> GetAllAsync()
        {
            var likeViewModel = new List<LikeViewModel>();
            
            try
            {
                var categories = await _repo.GetAllAsync();
                likeViewModel = categories.Select(c=> _mapper.Map<LikeViewModel>(c)).ToList();
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
            }

            return likeViewModel;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = false;
            try
            {
                var like = await _repo.GetAsync(id);
                result = await _repo.DeleteAsync(like);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
            }

            return result;
        }
    }
}