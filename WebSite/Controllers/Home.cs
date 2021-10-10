using DarsAsan.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DarsAsan.Controllers
{
    public class Home : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp([FromForm] SignUpViewModel model)
        {
            ViewData["statement"] = model.Email;
            return View("/test");
        }

        [HttpPost]
        public IActionResult SignIn()
        {
            return View();
        }

        public IActionResult Test()
        {
            return View();
        }
    }
}