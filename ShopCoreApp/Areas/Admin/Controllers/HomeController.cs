using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopCoreApp.Extensions;

namespace ShopCoreApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Dashboard";
            return View();
        }
    }
}