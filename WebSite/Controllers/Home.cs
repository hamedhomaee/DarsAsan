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

        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("sign-up")]
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

                string ConfirmEmailUrl = Url.ActionLink("ConfirmEmail", values: new { userId = NewUser.Id, token = Token }, protocol: Request.Scheme);

                string MailBody = "<h2 dir='rtl' style='margin: 0 auto;'>لطفا با کلیک بر روی لینک زیر ثبت‌نام خود را تایید کنید.</h2><br><a href='" + ConfirmEmailUrl + "'>Confirm Your Email</a></br>";

                MailSender.Program.SendMail("hamedhomaee1990@gmail.com", model.Email, "smtp.gmail.com", 587, MailBody, "Hamed Test Confirm Your Email", true, "uknown", true);

                return View("SignUpSuccess");

            }

            return View("Index", new HomeIndexViewModel() { Up = model });
        }

        [Route("sign-in")]
        [HttpPost]
        public async Task<IActionResult> SignInAsync([FromForm][Bind(Prefix = "In")] SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (await _userManager.FindByEmailAsync(model.SignInUsernameOrEmail) != null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult Result = await _signInManager.PasswordSignInAsync((await _userManager.FindByEmailAsync(model.SignInUsernameOrEmail)).UserName, model.SignInPassword, model.RememberMe, false);
                    if (Result.Succeeded)
                    {
                        ViewData["success"] = "You are logged in";
                        return RedirectToAction("Index");
                    }
                }
                else if (await _userManager.FindByNameAsync(model.SignInUsernameOrEmail) != null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult Result = await _signInManager.PasswordSignInAsync(model.SignInUsernameOrEmail, model.SignInPassword, model.RememberMe, false);
                    if (Result.Succeeded)
                    {
                        ViewData["success"] = "You are logged in";
                        return RedirectToAction("Index");
                    }
                }
                else
                    return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [Route("signup-success")]
        public IActionResult SignUpSuccess()
        {
            return View("Index");
        }

        [Route("confirm-email")]
        public async Task<IActionResult> ConfirmEmailAsync([FromQuery] string userId, [FromQuery] string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("Index");
            }

            ApplicationUser TheUser = await _userManager.FindByIdAsync(userId);

            if (TheUser == null)
            {
                ViewData["ErrorMessage"] = "کاربر یافت نشد. لطفا دوباره سعی کنید.";
                return View("NotFound");
            }

            var decodedTokenString = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));

            var Result = await _userManager.ConfirmEmailAsync(TheUser, decodedTokenString);

            if (!Result.Succeeded)
            {
                ViewData["ErrorMessage"] = "اشکالی رخ داده است، لطفا دوباره سعی کنید.";
                return NotFound();
            }

            ViewData["success"] = "your email is successfully confirmed.";
            return RedirectToAction("Index");
        }

        [Route("forgot-password")]
        [HttpPost]
        public async Task<IActionResult> ForgotPasswordAsync([FromForm][Bind] string email)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser TheUser = await _userManager.FindByEmailAsync(email);

                if (TheUser != null && await _userManager.IsEmailConfirmedAsync(TheUser))
                {
                    var Token = await _userManager.GeneratePasswordResetTokenAsync(TheUser);

                    Token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(Token));

                    string ResetPasswordUrl = Url.ActionLink("ResetPassword", values: new { email = TheUser.Email, token = Token }, protocol: Request.Scheme);

                    string MailBody = "<h2 dir='rtl' style='margin: 0 auto;'>لطفا برای بازیابی کلمه عبور بر روی لینک زیر کلیک کنید.</h2><br><a href='" + ResetPasswordUrl + "'>بازیابی رمز عبور</a></br>";

                    MailSender.Program.SendMail("hamedhomaee1990@gmail.com", TheUser.Email, "smtp.gmail.com", 587, MailBody, "Hamed Test Confirm Your Email", true, "uknown", true);

                    return View();
                }
                return View("");
            }
            return View("");
        }

        [Route("reset-password")]
        public IActionResult ResetPassword([FromQuery] string userEmail, [FromQuery] string token)
        {
            if (string.IsNullOrWhiteSpace(userEmail) || string.IsNullOrWhiteSpace(token))
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        [Route("confirm-reset-password")]
        [HttpPost]
        public async Task<IActionResult> ConfirmResetPasswordAsync([FromForm][Bind] ResetPasswordViewModel model)
        {
            if (ModelState.IsValid && !string.IsNullOrEmpty(model.Token))
            {
                ApplicationUser theUser = await _userManager.FindByEmailAsync(model.Email);

                if (theUser != null)
                {
                    var result = await _userManager.ResetPasswordAsync(theUser, model.Token, model.Password);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }

            return View(model);
        }

        [Route("not-signed-in")]
        public IActionResult NotSignedIn()
        {
            return RedirectToAction("Index");
        }
    }
}