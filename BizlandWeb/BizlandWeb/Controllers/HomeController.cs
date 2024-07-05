using Microsoft.AspNetCore.Mvc;

namespace BizlandWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
