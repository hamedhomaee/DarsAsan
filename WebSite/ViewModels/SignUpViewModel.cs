using System.ComponentModel.DataAnnotations;

namespace DarsAsan.ViewModels
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "نام کاربری باید پر شود")]
        [StringLength(30)]
        [Display(Name = "نام کاربری")]
        public string Username { get; set; }

        [Required(ErrorMessage = "آدرس ایمیل باید پر شود")]
        [EmailAddress(ErrorMessage = "آدرس ایمیل اشتباه است")]
        [Display(Name = "آدرس ایمیل")]
        public string Email { get; set; }

        [Display(Name = "تکرار آدرس ایمیل")]
        [Compare("Email", ErrorMessage = "ایمیل همسان نیست")]
        public string ConfirmEmail { get; set; }

        [Phone]
        [Display(Name = "شماره موبایل (اختیاری)")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "رمز عبور باید پر شود")]
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