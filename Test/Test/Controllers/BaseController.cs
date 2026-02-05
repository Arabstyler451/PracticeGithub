using Microsoft.AspNetCore.Mvc;

namespace Test.Controllers
{
    public class BaseController : Controller
    {
        protected string GetLayout()
        {
            if (User.IsInRole("Admin"))
            {
                return "_AdminLayout";
            }
            return "_Layout";
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
