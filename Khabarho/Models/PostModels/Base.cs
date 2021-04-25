using System;
using System.ComponentModel.DataAnnotations;

namespace Khabarho.Models.PostModels
{
    public abstract class Base
    {
        public Guid Id { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        public DateTime CreatedDate  {get; set; }

        public DateTime? UpdatedDate { get; set; }
        
        public bool IsDeleted { get; set; }
        
        public DateTime? DeletedDate { get; set; }
    }
}