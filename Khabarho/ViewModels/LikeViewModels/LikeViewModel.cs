using System;

namespace Khabarho.ViewModels.LikeViewModels
{
    public class LikeViewModel
    {
        public Guid Id { get; set; }
        
        public DateTime CreatedDate  {get; set; }

        public string UserId { get; set; }
        
        public string UserName { get; set; }

        public Guid PostId { get; set; }
    }
}