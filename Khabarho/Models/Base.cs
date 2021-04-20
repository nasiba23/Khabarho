using System;

namespace Khabarho.Models
{
    public abstract class Base
    {
        public Guid Id { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}