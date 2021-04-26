using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Khabarho.Models.PostModels;

namespace Khabarho.ViewModels.PostViewModels
{
    public class ShowPostViewModel : BasePostViewModel
    {
        [Required]
        public string AuthorName { get; set; }
        
        [Required]
        public List<Category> Categories { get; set; }

        [Required]
        public Type Type { get; set; }
        
        public List<Comment> Comments { get; set; }
        
        public long NumberOfComments { get; set; }

        public long NumberOfLikes { get; set; }
    }
}