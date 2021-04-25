using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Khabarho.Models.PostModels
{
    public class Comment : BaseReaction
    {
        [Required]
        public string Text { get; set; }
    }
}