using Microsoft.AspNetCore.Mvc;
using RetroRealm.Data;
using RetroRealm.Models;

namespace RetroRealm.Controllers
{
    public class TechnicianController : Controller
    {
        public ApplicationDbContext Context { get; set; }
        public TechnicianController(ApplicationDbContext ctx) => Context = ctx;

        [HttpGet("technicians")]
        public IActionResult List()
        {
            List<TechnicianModel> technicians = Context.Technicians.Where(t => t.TechnicianModelId != -1)
            .OrderBy(g => g.Name).ToList();
            return View(technicians);
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("Edit", new TechnicianModel());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            TechnicianModel? technician = Context.Technicians.Find(id);
            return View("Edit", technician);
        }

        [HttpPost]
        public IActionResult Edit(TechnicianModel technician)
        {
            if (ModelState.IsValid)
            {
                if (technician.TechnicianModelId == 0)
                    Context.Technicians.Add(technician);
                else
                    Context.Technicians.Update(technician);
                Context.SaveChanges();
                return RedirectToAction("List");
            }
            ViewBag.Action = (technician.TechnicianModelId == 0) ? "Add" : "Edit";
            return View(technician);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            TechnicianModel? technician = Context.Technicians.Find(id);
            return View(technician);
        }
        [HttpPost]
        public IActionResult Delete(TechnicianModel technician)
        {
            Context.Technicians.Remove(technician);
            Context.SaveChanges();
            return RedirectToAction("List");
        }
    }
}
