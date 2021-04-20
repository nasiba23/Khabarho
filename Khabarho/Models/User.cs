using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Khabarho.Models
{
    public class User: IdentityUser
    {
        public ICollection<Post> Posts { get; set; }
        
        public ICollection<Like> Likes { get; set; }
        
        public ICollection<Comment> Comments { get; set; }
        
        public DateTime? UpdatedDate { get; set; }
        
        public bool IsDeleted { get; set; }
        
        public DateTime? DeletedDate { get; set; }
    }
}