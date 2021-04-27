using System;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using Khabarho.Services.CategoryService;
using Khabarho.Services.PostService;
using Khabarho.Services.TypeService;
using Khabarho.ViewModels.PostViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Khabarho.Controllers
{
    public class PostController : Controller
    {
        private IPostService _postService;
        private ICategoryService _categoryService;
        private ITypeService _typeService;
        private IWebHostEnvironment _webHostEnvironment;

        public PostController(IPostService postService, 
                             ICategoryService categoryService, 
                             ITypeService typeService, IWebHostEnvironment webHostEnvironment)
        {
            _postService = postService;
            _categoryService = categoryService;
            _typeService = typeService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAllAsync();
            var types = await _typeService.GetAllAsync();
            ViewBag.Categories = categories;
            ViewBag.Types = types;
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(PostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            model.AuthorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            var result = await _postService.CreateAsync(model);

            return RedirectToAction("ShowPost", "Post", new {id = result.Id});
        }

        [HttpGet]
        public async Task<IActionResult> ShowPost(string id)
        {
            var model = await _postService.GetAsync(id);
            
            if (model.Id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            
            ViewBag.Post = model;
            
            return View();
        }
    }
}