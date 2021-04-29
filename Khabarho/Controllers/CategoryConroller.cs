using System.Threading.Tasks;
using Khabarho.Extensions;
using Khabarho.Services.CategoryService;
using Khabarho.Utilities;
using Khabarho.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Khabarho.Controllers
{
    
    [ServiceFilter(typeof(CustomFilterAttribute))]
    public class CategoryController : Controller
    {
        private ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }
        
        [HttpGet]
        [Authorize (Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            var result = await _service.CreateAsync(model);
        
            return RedirectToAction("GetAll", "Category");
        
        }
        
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _service.GetAllAsync();
        
            categories.CustomNullCheck(ErrorMessages.NotFoundError);
            
            return View(categories);
        }
        
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(string id)
        {
            id.CustomNullCheck(ErrorMessages.NullParameterError);
        
            var category = await _service.GetAsync(id);
            
            category.CustomNullCheck(ErrorMessages.NotFoundError);
            
            return View(category);
        }
        
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(string id)
        {
            return View();
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            await _service.UpdateAsync(model);
        
            return RedirectToAction("GetAll", "Category");
        }
        
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            id.CustomNullCheck(ErrorMessages.NullParameterError);
            
            await _service.DeleteAsync(id);
        
            return RedirectToAction("GetAll", "Category");
        }
    }
}