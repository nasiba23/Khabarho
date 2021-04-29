using System.Security.Claims;
using System.Threading.Tasks;
using Khabarho.Repositories;
using Khabarho.Services.CommentService;
using Khabarho.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Khabarho.Controllers
{
    public class CommentController : Controller
    {
        private ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateComment(CommentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            
            model.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _commentService.CreateAsync(model);
            
            return RedirectToAction("ShowPost", "Post", new {id = model.PostId});
        }

        [HttpPost]
        [Authorize]
        [Route("~/Comment/DeleteAsync")]
        public async Task<IActionResult> DeleteAsync(CommentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ShowPost", "Post");
            }
            
            model.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _commentService.DeleteAsync(model.Id.ToString());
            
            return Json(new { success = "done"});
        }

        [HttpPost]
        [Authorize]
        [Route("~/Comment/EditAsync")]
        public async Task<IActionResult> EditAsync(CommentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ShowPost", "Post");
            }
            
            model.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _commentService.UpdateAsync(model);

            return Json(result);
        }
    }
}