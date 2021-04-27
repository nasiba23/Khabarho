using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Khabarho.Db;
using Khabarho.Models.PostModels;
using Khabarho.Repositories;
using Khabarho.Services.TypeService;
using Khabarho.ViewModels.PostViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Logging;

namespace Khabarho.Services.PostService
{
    public class PostService : IPostService
    {
        private IBaseRepository<Post> _repo;
        private IMapper _mapper;
        private ILogger<PostService> _logger;
        private IWebHostEnvironment _webHostEnvironment;
        private DataContext _context;

        public PostService(IBaseRepository<Post> repo, IMapper mapper,
                            ILogger<PostService> logger, IWebHostEnvironment webHostEnvironment,
                            DataContext context)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        
        public async Task<ShowPostViewModel> CreateAsync(PostViewModel model)
        {
            var showPostViewModel = new ShowPostViewModel();
            
            try
            {
                model.CreatedDate = DateTime.Now;
                string imagePath = null;

                if (model.ImageFile != null)
                {
                    imagePath = await CopyFileAsync(model.ImageFile);
                }
                
                // model.Type = await _context.Types.FirstOrDefaultAsync(t => t.Id == model.TypeId);
                // model.Categories =  await _context.Categories.Where(c => model.CategoriesId.Contains(c.Id)).ToListAsync();
                
                var post = _mapper.Map<Post>(model);
                
                post.Image = imagePath ?? "";
                
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
        
        private async Task<string> CopyFileAsync(IFormFile imageFile)
        {
            if (imageFile == null) return null;

            var rootPath = _webHostEnvironment.WebRootPath;
            var filename = Path.GetFileNameWithoutExtension(imageFile.FileName);
            var fileExtension = Path.GetExtension(imageFile.FileName);
            var finalFileName = $"{filename}_{DateTime.Now.ToString("yyMMddHHmmssff")}{fileExtension}";
            var filePath = Path.Combine(rootPath, "images", finalFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return finalFileName;
        }
    }
}