using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Khabarho.Models.PostModels;

namespace Khabarho.ViewModels
{
    public class CategoryViewModel
    {
        public Guid Id { get; set; }
        
        [Required]
        public string Title { get; set; }

        public DateTime CreatedDate { get; set; }
        
        public List<Post> Posts { get; set; }
    }
}