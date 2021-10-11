using System.Threading.Tasks;
using DarsAsan.Models;
using DarsAsan.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DarsAsan.Controllers
{
    public class Home : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public Home(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
                return View(model);
            }

            if (model.IsTeacher == true)
            {
                ApplicationUser NewUser = new TeacherUser()
                {
                    UserName = model.Username,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber
                };

                var Result = await _userManager.CreateAsync(NewUser, model.Password);

                if (Result.Succeeded)
                {
                    return View("SignUpSuccess");
                }

                return View(model);
            }
            else
            {
                ApplicationUser NewUser = new StudentUser()
                {
                    UserName = model.Username,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber
                };

                var Result = await _userManager.CreateAsync(NewUser, model.Password);

                if (Result.Succeeded)
                {
                    return View("SignUpSuccess");
                }

                return View(model);
            }
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