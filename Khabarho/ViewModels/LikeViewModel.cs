using System;
using System.ComponentModel.DataAnnotations;

namespace Khabarho.ViewModels
{
    public class LikeViewModel
    {
        public Guid Id { get; set; }
        
        public DateTime CreatedDate  {get; set; }
        
        [Required]
        public string UserId { get; set; }
        
        [Required]
        public Guid PostId { get; set; }
    }
}