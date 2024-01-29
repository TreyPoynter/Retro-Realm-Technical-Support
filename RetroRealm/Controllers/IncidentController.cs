using Microsoft.AspNetCore.Mvc;

namespace RetroRealm.Controllers
{
    public class IncidentController : Controller
    {
        [HttpGet]
        public IActionResult List()
        {
            return View();
        }
    }
}
