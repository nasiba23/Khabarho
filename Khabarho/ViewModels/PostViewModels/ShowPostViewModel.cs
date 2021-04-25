using System.Collections.Generic;
using Khabarho.Models.PostModels;

namespace Khabarho.ViewModels.PostViewModels
{
    public class ShowPostViewModel : BasePostViewModel
    {
        public string AuthorName { get; set; }
        
        public List<Category> Categories { get; set; }

        public Type Type { get; set; }
        
        public List<Comment> Comments { get; set; }
        
        public uint NumberOfComments { get; set; }

        public uint NumberOfLikes { get; set; }
    }
}