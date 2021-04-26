using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Khabarho.Models.PostModels
{
    public class Type : Base
    {
        public virtual ICollection<Post> Posts { get; set; }
    }
}