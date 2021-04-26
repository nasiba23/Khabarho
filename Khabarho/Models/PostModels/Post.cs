using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Khabarho.Models.AccountModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Khabarho.Models.PostModels
{
    public class Post : Base
    {
        public string AuthorId { get; set; }
        
        [Required]
        public Guid TypeId { get; set; }
        
        [Required]
        public virtual ICollection<Category> Categories { get; set; }
        
        [Required]
        public string Image { get; set; }
        
        [Required]
        public string Text { get; set; }

        public long NumberOfLikes => Likes.Count;

        public long NumberOfComments => Comments.Count;

        public virtual ICollection<Like> Likes { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        
        [ForeignKey("AuthorId")]
        public virtual User Author { get; set; }
        
        [ForeignKey("TypeId")]
        public virtual Type Type { get; set; }
    }
}