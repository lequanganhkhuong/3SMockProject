using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MockProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class HomeController : Controller
    {
        // GET
        public IActionResult Index()
        {
            ViewBag.Pages = "Dashboard";
            return View();
        }
    }
}