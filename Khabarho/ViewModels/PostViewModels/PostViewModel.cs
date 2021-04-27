using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Khabarho.Models.PostModels;
using Microsoft.AspNetCore.Http;
using Type = Khabarho.Models.PostModels.Type;

namespace Khabarho.ViewModels.PostViewModels
{
    public class PostViewModel
    {
        public Guid? Id { get; set; }
        
        public DateTime? CreatedDate { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        public string ImagePath { get; set; }
        
        public string Text { get; set; }
        
        public Guid TypeId { get; set; }
        
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }
        
        public List<Category> Categories { get; set; }
        
        public List<Guid> CategoriesId { get; set; }
        
        public string AuthorId { get; set; }

        public string AuthorName { get; set; }
        
        public Type Type { get; set; }
        
        public List<Comment> Comments { get; set; }
        
        public List<Like> Likes { get; set; }
        
        public long NumberOfComments { get; set; }

        public long NumberOfLikes { get; set; }
    }
}