using System.ComponentModel.DataAnnotations;

namespace Khabarho.ViewModels.Account
{
    public class LoginViewModel : BaseAccountViewModel
    {
        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }
        
        public string ReturnUrl { get; set; }
    }
}