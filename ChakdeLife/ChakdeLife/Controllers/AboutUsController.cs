using Microsoft.AspNetCore.Mvc;

namespace ChakdeLife.Controllers
{
    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
