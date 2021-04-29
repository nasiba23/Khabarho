using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Khabarho.Extensions;
using Khabarho.Services.CategoryService;
using Khabarho.Services.PostService;
using Khabarho.Services.TypeService;
using Khabarho.Utilities;
using Khabarho.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Khabarho.Controllers
{
    [ServiceFilter(typeof(CustomFilterAttribute))]
    public class PostController : Controller
    {
        private IPostService _postService;
        private ICategoryService _categoryService;
        private ITypeService _typeService;

        public PostController(IPostService postService, 
                             ICategoryService categoryService, 
                             ITypeService typeService)
        {
            _postService = postService;
            _categoryService = categoryService;
            _typeService = typeService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
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
        
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllByUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var posts = (await _postService.GetAllAsync()).Where(x => x.AuthorId.ToString() == userId).ToList();

            return View(posts);
        }
        
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Update(string id)
        {
            var post = await _postService.GetAsync(id);
                
            return View(post);
        }
        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Update(PostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
        
            model.AuthorId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _postService.UpdateAsync(model);
        
            return RedirectToAction("GetAllByUserId", "Post");
        }
        
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            id.CustomNullCheck(ErrorMessages.NullParameterError);

            await _postService.DeleteAsync(id);
        
            return RedirectToAction("GetAllByUserId", "Post");
        }
    }
}