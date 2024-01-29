using Microsoft.AspNetCore.Mvc;
using RetroRealm.Models;

namespace RetroRealm.Controllers
{
    public class TechnicianController : Controller
    {
        public GameModelContext Context { get; set; }
        public TechnicianController(GameModelContext ctx) => Context = ctx;
        [HttpGet]
        public IActionResult List()
        {
            List<TechnicianModel> technicians = Context.Technicians.OrderBy(g => g.Name).ToList();
            return View(technicians);
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("Edit", new TechnicianModel());
        }

        [HttpGet]
        public IActionResult Edit()
        {
            ViewBag.Action = "Edit";
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            TechnicianModel? technician = Context.Technicians.Find(id);
            return View(technician);
        }
    }
}
