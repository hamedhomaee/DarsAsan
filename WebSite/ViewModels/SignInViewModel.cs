using System.ComponentModel.DataAnnotations;

namespace DarsAsan.ViewModels
{
    public class SignInViewModel
    {
        [Required(ErrorMessage = "نام کاربری باید پر شود")]
        [StringLength(30, ErrorMessage = "نام‌کاربری مجاز حداکثر 30 کاراکتر میباشد.")]
        [Display(Name = "نام کاربری")]
        public string SignInUsernameOrEmail { get; set; }

        [Required(ErrorMessage = "رمز عبور باید پر شود")]
        [StringLength(30, ErrorMessage = "رمزعبور مجاز حداکثر 30 کاراکتر میباشد.")]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string SignInPassword { get; set; }

        [Display(Name = "مرا به خاطر بسپار؟")]
        public bool RememberMe { get; set; }
    }
}