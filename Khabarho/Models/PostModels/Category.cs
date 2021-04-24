using System.Collections.Generic;

namespace Khabarho.Models.PostModels
{
    public class Category : Base
    {
        public ICollection<Post> Posts { get; set; }
    }
}