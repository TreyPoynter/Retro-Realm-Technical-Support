using Microsoft.AspNetCore.Mvc;
using RetroRealm.Models;
using System.Diagnostics;

namespace RetroRealm.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("about")]
        public IActionResult About()
        {
            return View();
        }
    }
}
