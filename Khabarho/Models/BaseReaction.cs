using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Khabarho.Models
{
    public abstract class BaseReaction
    {
        public Guid Id { get; set; }
        
        public string UserId { get; set; }

        public Guid PostId { get; set; }
        
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        
        [ForeignKey("PostId")]
        public virtual Post Post { get; set; }
    }
}