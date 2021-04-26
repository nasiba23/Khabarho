using System.Security.Claims;
using System.Threading.Tasks;
using Khabarho.Repositories;
using Khabarho.Services.CommentService;
using Khabarho.ViewModels.CommentViewModels;
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
    }
}