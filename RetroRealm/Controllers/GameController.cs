using Microsoft.AspNetCore.Mvc;

namespace RetroRealm.Controllers
{
    public class GameController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ManageGames()
        {
            return View();
        }
    }
}
