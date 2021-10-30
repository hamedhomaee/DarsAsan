using System.ComponentModel.DataAnnotations;

namespace DarsAsan.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور جدید")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تکرار کلمه عبور")]
        [Compare("Password", ErrorMessage = "کلمه عبور یکسان نیست")]
        public string RePassword { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Token { get; set; }
    }
}