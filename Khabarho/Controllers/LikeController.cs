using System.Security.Claims;
using System.Threading.Tasks;
using Khabarho.Services.LikeService;
using Khabarho.Utilities;
using Khabarho.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Khabarho.Controllers
{
    
    [ServiceFilter(typeof(CustomFilterAttribute))]
    public class LikeController : Controller
    {
        private ILikeService _service;

        public LikeController(ILikeService service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize]
        public IActionResult Check(LikeViewModel model)
        {
            return Json(_service.LikeCheck(model) ? new {isLiked = true} : new {isLiked = false});
        }
        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Click(LikeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Showpost", "Post");
            }
            
            model.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            if (!_service.LikeCheck(model))
            {
                await _service.CreateAsync(model);
                return Json(new { isLiked = true});
            }
            
            await _service.DeleteAsync(model);
            return Json(new { isLiked = false});
        }
    }
}