using System.ComponentModel.DataAnnotations;

namespace Khabarho.ViewModels.Account
{
    public abstract class BaseAccountViewModel
    {
        [Required]
        [Display(Name = "Никнейм")]
        public string UserName { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}