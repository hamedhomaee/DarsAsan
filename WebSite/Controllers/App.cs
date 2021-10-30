using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DarsAsan.Controllers
{
    [Authorize]
    public class App : Controller
    {
        [Route("portal")]
        [Authorize]
        public IActionResult Portal()
        {
            return View();
        }
    }
}