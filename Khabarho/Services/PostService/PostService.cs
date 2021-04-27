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
        
        public async Task<PostViewModel> CreateAsync(PostViewModel model)
        {
            var postViewModel = new PostViewModel();
            
            try
            {
                model.CreatedDate = DateTime.Now;
                string imagePath = null;

                if (model.ImageFile != null)
                {
                    imagePath = await CopyFileAsync(model.ImageFile);
                }
                
                //didn't map
                model.Type = await _context.Types.FirstOrDefaultAsync(t => t.Id == model.TypeId);
                model.Categories =  await _context.Categories.Where(c => model.CategoriesId.Contains(c.Id)).ToListAsync();
                var author = await _context.Users.FirstOrDefaultAsync(x => x.Id == model.AuthorId);
                model.AuthorName = author.UserName;
                
                var post = _mapper.Map<Post>(model);
                
                post.Image = imagePath ?? "";
                
                var result = await _repo.InsertAsync(post);
                
                if (!result)
                {
                    return new PostViewModel();
                }
                
                postViewModel = _mapper.Map<PostViewModel>(post);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
            }

            return postViewModel;
        }

        public async Task<PostViewModel> GetAsync(string id)
        {
            var postViewModel = new PostViewModel();
            
            try
            { 
                var post = await _repo.GetAsync(id);
                
                postViewModel = _mapper.Map<PostViewModel>(post);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
            }

            return postViewModel;
        }

        public async Task<List<PostViewModel>> GetAllAsync()
        {
            var postsViewModel = new List<PostViewModel>();
            
            try
            {
                var posts = await _repo.GetAllAsync();

                postsViewModel = posts.Select(c=> _mapper.Map<PostViewModel>(c)).ToList();;
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
            }

            return postsViewModel;
        }

        public  async Task<PostViewModel> UpdateAsync(PostViewModel model)
        {
            var postViewModel = new PostViewModel();
            
            try
            {
                var post = _mapper.Map<Post>(model);
                var result = await _repo.UpdateAsync(post);
                
                if (!result)
                {
                    return new PostViewModel();
                }
                
                postViewModel = _mapper.Map<PostViewModel>(post);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
            }

            return postViewModel;
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