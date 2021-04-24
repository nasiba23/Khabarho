using System.ComponentModel.DataAnnotations;

namespace Khabarho.ViewModels.Account
{
    public class RegisterViewModel : BaseAccountViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль ещё раз")]
        public string PasswordConfirm { get; set; }
    }
}