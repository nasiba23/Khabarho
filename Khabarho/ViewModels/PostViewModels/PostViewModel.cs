using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Khabarho.ViewModels.PostViewModels
{
    public class PostViewModel : BasePostViewModel
    {
        [Required]
        public Guid TypeId { get; set; }
        
        public List<SelectListItem> Types { get; set; } = new List<SelectListItem>();
        
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }

        public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();

        public string AuthorId { get; set; }
    }
}