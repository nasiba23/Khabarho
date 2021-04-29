using System;
using System.ComponentModel.DataAnnotations;

namespace Khabarho.ViewModels
{
    public class CommentViewModel
    {
        public Guid Id { get; set; }
        
        public DateTime CreatedDate  {get; set; }

        public string UserId { get; set; }
        
        public string UserName { get; set; }
        
        [Required]
        public string Text { get; set; }

        public Guid PostId { get; set; }
    }
}