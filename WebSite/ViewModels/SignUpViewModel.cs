using System.ComponentModel.DataAnnotations;

namespace DarsAsan.ViewModels
{
    public class SignUpViewModel
    {
        [Required]
        [StringLength(30)]
        [Display(Name = "نام کاربری")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "آدرس ایمیل")]
        public string Email { get; set; }

        [EmailAddress]
        [Display(Name = "تکرار آدرس ایمیل")]
        [Compare("Email", ErrorMessage = "ایمیل همسان نیست")]
        public string ConfirmEmail { get; set; }

        [Phone]
        [Display(Name = "شماره موبایل (اختیاری)")]
        [MaxLength(12)]
        public int MyProperty { get; set; }

        [Required]
        [StringLength(30)]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [StringLength(30)]
        [Compare("Password", ErrorMessage = "رمز عبور همسان نیست")]
        [Display(Name = "تکرار رمز عبور")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "معلم هستم")]
        public bool IsTeacher { get; set; }
    }
}