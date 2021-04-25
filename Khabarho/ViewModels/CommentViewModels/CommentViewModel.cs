using System;

namespace Khabarho.ViewModels.CommentViewModels
{
    public class CommentViewModel
    {
        public Guid Id { get; set; }
        
        public string UserId { get; set; }
        
        public string UserName { get; set; }

        public Guid PostId { get; set; }
    }
}