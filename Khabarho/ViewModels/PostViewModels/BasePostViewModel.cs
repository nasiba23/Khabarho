using System;
using System.ComponentModel.DataAnnotations;

namespace Khabarho.ViewModels.PostViewModels
{
    public abstract class BasePostViewModel
    {
        public Guid? Id { get; set; }
        
        public DateTime? CreatedDate { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        public string ImagePath { get; set; }
        
        public string Text { get; set; }
    }
}