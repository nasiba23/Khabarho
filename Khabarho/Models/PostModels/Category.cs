using System.Collections.Generic;

namespace Khabarho.Models.PostModels
{
    public class Category : Base
    {
        public virtual ICollection<Post> Posts { get; set; }
    }
}