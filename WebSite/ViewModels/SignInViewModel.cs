using System.ComponentModel.DataAnnotations;

namespace DarsAsan.ViewModels
{
    public class SignInViewModel
    {
        [Required]
        [StringLength(30)]
        [Display(Name = "نام کاربری")]
        public string SignInUsernameOrEmail { get; set; }

        [Required]
        [StringLength(30)]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string SignInPassword { get; set; }

        [Display(Name = "مرا به خاطر بسپار؟")]
        public bool RememberMe { get; set; }
    }
}