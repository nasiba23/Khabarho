using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Khabarho.Models.PostModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Type = System.Type;

namespace Khabarho.ViewModels.PostViewModels
{
    public class PostViewModel : BasePostViewModel
    {
        [Required]
        public Guid TypeId { get; set; }
        
        public Models.PostModels.Type Type { get; set; }
        
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }

        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Guid> CategoriesId { get; set; }
        
        public string AuthorId { get; set; }
    }
}