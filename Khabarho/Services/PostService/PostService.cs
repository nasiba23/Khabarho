using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Khabarho.Models.PostModels;
using Khabarho.Repositories;
using Khabarho.ViewModels.PostViewModels;
using Microsoft.Extensions.Logging;

namespace Khabarho.Services.PostService
{
    public class PostService : IPostService
    {
        private IBaseRepository<Post> _repo;
        private IMapper _mapper;
        private ILogger<PostService> _logger;

        public PostService(IBaseRepository<Post> repo, IMapper mapper, ILogger<PostService> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }
        
        public async Task<ShowPostViewModel> CreateAsync(PostViewModel model)
        {
            var showPostViewModel = new ShowPostViewModel();
            
            try
            {
                var post = _mapper.Map<Post>(model);
                var result = await _repo.InsertAsync(post);
                
                if (!result)
                {
                    return new ShowPostViewModel();
                }
                
                showPostViewModel = _mapper.Map<ShowPostViewModel>(post);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
            }

            return showPostViewModel;
        }

        public async Task<ShowPostViewModel> GetAsync(string id)
        {
            var showPostViewModel = new ShowPostViewModel();
            
            try
            { 
                var post = await _repo.GetAsync(id);
                showPostViewModel = _mapper.Map<ShowPostViewModel>(post);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
            }

            return showPostViewModel;
        }

        public async Task<List<ShowPostViewModel>> GetAllAsync()
        {
            var showPostsViewModel = new List<ShowPostViewModel>();
            
            try
            {
                var posts = await _repo.GetAllAsync();
                showPostsViewModel = posts.Select(c=> _mapper.Map<ShowPostViewModel>(c)).ToList();;
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
            }

            return showPostsViewModel;
        }

        public  async Task<ShowPostViewModel> UpdateAsync(PostViewModel model)
        {
            var showPostViewModel = new ShowPostViewModel();
            
            try
            {
                var post = _mapper.Map<Post>(model);
                var result = await _repo.UpdateAsync(post);
                
                if (!result)
                {
                    return new ShowPostViewModel();
                }
                
                showPostViewModel = _mapper.Map<ShowPostViewModel>(post);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
            }

            return showPostViewModel;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = false;
            try
            {
                var post = await _repo.GetAsync(id);
                result = await _repo.DeleteAsync(post);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
            }

            return result;
        }
    }
}