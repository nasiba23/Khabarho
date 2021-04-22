using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Khabarho.Models
{
    public class Type : Base
    {
        public ICollection<Post> Posts { get; set; }
    }
}