using System;
using System.Collections.Generic;
using Khabarho.Models.PostModels;
using Microsoft.AspNetCore.Identity;

namespace Khabarho.Models.AccountModels
{
    public class User: IdentityUser
    {
        public virtual ICollection<Post> Posts { get; set; }
        
        public virtual ICollection<Like> Likes { get; set; }
        
        public virtual ICollection<Comment> Comments { get; set; }
        
        public DateTime? UpdatedDate { get; set; }
        
        public bool IsDeleted { get; set; }
        
        public DateTime? DeletedDate { get; set; }
    }
}