using System.Text;
using System.Threading.Tasks;
using DarsAsan.Models;
using DarsAsan.Utilities;
using DarsAsan.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace DarsAsan.Controllers
{
    public class Home : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMailSender _mailSender;

        public Home(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMailSender mailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mailSender = mailSender;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUpAsync([FromForm][Bind(Prefix = "Up")] SignUpViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", new HomeIndexViewModel() { Up = model });
            }

            ApplicationUser NewUser = model.IsTeacher ?
            new TeacherUser()
            {
                UserName = model.Username,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber
            } :
            new TeacherUser()
            {
                UserName = model.Username,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber
            };

            var Result = await _userManager.CreateAsync(NewUser, model.Password);

            if (Result.Succeeded)
            {
                string Token = await _userManager.GenerateEmailConfirmationTokenAsync(NewUser);
                Token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(Token));

                string ConfirmEmailUrl = Url.ActionLink("ConfirmEmail", values: new { userId = NewUser.Id, token = Token });

                string MailBody = "<h2 style='margin: 0 auto;'>Please Confirm Your Email</h2><br><a href='" + ConfirmEmailUrl + "'>Confirm Your Email</a></br>";

                MailSender.Program.SendMail("hamedhomaee1990@gmail.com", model.Email, "smtp.gmail.com", 587, MailBody, "Hamed Test Confirm Your Email", true, "hamedhomaee1990@gmail.com", "noonecanknow", true);

                return View("SignUpSuccess");

            }

            return View("Index", new HomeIndexViewModel() { Up = model });
        }

        [HttpPost]
        public IActionResult SignIn()
        {
            return View();
        }

        public IActionResult SignUpSuccess()
        {
            return View();
        }
    }
}