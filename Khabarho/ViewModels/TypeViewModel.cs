using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Khabarho.Models.PostModels;

namespace Khabarho.ViewModels
{
    public class TypeViewModel
    {
        public Guid Id { get; set; }
        
        public DateTime CreatedDate  {get; set; }

        [Required]
        public string Title { get; set; }
        
        public List<Post> Posts { get; set; }
    }
}