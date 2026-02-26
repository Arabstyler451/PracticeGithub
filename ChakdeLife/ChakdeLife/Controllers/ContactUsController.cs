using Microsoft.AspNetCore.Mvc;

namespace ChakdeLife.Controllers
{
    public class ContactUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
