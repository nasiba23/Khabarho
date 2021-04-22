using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Khabarho.Models
{
    public class Category : Base
    {
        public ICollection<Post> Posts { get; set; }
    }
}