using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Khabarho.Models;
using Khabarho.Services.CategoryService;
using Khabarho.Services.PostService;
using Khabarho.Services.TypeService;
using Khabarho.Utilities;

namespace Khabarho.Controllers
{
    [ServiceFilter(typeof(CustomFilterAttribute))]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ICategoryService _categoryService;
        private ITypeService _typeService;
        private IPostService _postService;

        public HomeController(IPostService postService, ILogger<HomeController> logger, ICategoryService categoryService, ITypeService typeService)
        {
            _logger = logger;
            _categoryService = categoryService;
            _typeService = typeService;
            _postService = postService;
        }
        
        public async Task<IActionResult> Index()
        {
            ViewBag.Posts = (await _postService.GetAllAsync())
                .OrderByDescending(x => x.CreatedDate)
                .ToList();
            
            return View();
        }
        

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}