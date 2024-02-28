using Microsoft.AspNetCore.Mvc;
using RetroRealm.Migrations;
using RetroRealm.Models;

namespace RetroRealm.Controllers
{
    public class TechIncidentController : Controller
    {
        public GameModelContext Context { get; set; }
        public TechIncidentController(GameModelContext ctx) => Context = ctx;
        public IActionResult Index()
        {
            List<TechnicianModel> technicians = Context.Technicians.Where(t => t.TechnicianModelId != -1)
            .OrderBy(g => g.Name).ToList();
            return View(technicians);
        }

        [HttpPost]
        public IActionResult SearchIncidents()
        {
            return RedirectToAction("Index");
        }
    }
}
