using Microsoft.AspNetCore.Mvc;

namespace DarsAsan.Controllers
{
    public class Home : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}