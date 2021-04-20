using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Khabarho.Models
{
    public class Comment : BaseReaction
    {
        public DateTime? UpdatedDate { get; set; }
    }
}