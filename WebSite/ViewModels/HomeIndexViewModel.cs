using System.ComponentModel.DataAnnotations;

namespace DarsAsan.ViewModels
{
    public class HomeIndexViewModel
    {
        public SignInViewModel In { get; set; }
        public SignUpViewModel Up { get; set; }

        [DataType(DataType.EmailAddress)]
        public string ForgotPasswordEmail { get; set; }
    }
}