﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Khabarho.Models
{
    public class Category : Base
    {
        [Required]
        public string Title { get; set; }
        
        public ICollection<Post> Posts { get; set; }
    }
}