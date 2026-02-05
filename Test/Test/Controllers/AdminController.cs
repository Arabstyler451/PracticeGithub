using Microsoft.AspNetCore.Mvc;

namespace Test.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
