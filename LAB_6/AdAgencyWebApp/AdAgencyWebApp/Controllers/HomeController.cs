using Microsoft.AspNetCore.Mvc;

namespace AdAgencyWebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}