using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetroRealm.Migrations;
using RetroRealm.Models;
using System.Linq;

namespace RetroRealm.Controllers
{
    public class IncidentController : Controller
    {
        public GameModelContext Context { get; set; }
        public IncidentController(GameModelContext ctx) => Context = ctx;

        [HttpGet("incidents")]
        public IActionResult List()
        {
            List <IncidentModel> incidents = Context.Incidents.Include(i => i.Customer).Include(i => i.Game).ToList();
            return View(incidents);
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            GetViewBagOptions();
            return View("Edit", new IncidentModel());
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            GetViewBagOptions();
            IncidentModel? incident = Context.Incidents.Find(id);
            return View("Edit", incident);
        }

        [HttpPost]
        public IActionResult Edit(IncidentModel incident)
        {
            GetViewBagOptions();
            if (ModelState.IsValid)
            {
                if (incident.IncidentModelId == 0)
                    Context.Incidents.Add(incident);
                else
                    Context.Incidents.Update(incident);
                Context.SaveChanges();
                return RedirectToAction("List");
            }
            if (incident.DateClosed > DateTime.Now)
            {
                ModelState.AddModelError("DateClosed", "Closed date cannot be in the future.");
            }
            ViewBag.Action = (incident.IncidentModelId == 0) ? "Add" : "Edit";
            return View(incident);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            IncidentModel? incident = Context.Incidents.Find(id);
            return View(incident);
        }
        [HttpPost]
        public IActionResult Delete(IncidentModel incident)
        {
            Context.Incidents.Remove(incident);
            Context.SaveChanges();
            return RedirectToAction("List");
        }

        void GetViewBagOptions()
        {
            ViewBag.Customers = Context.Customers.OrderBy(c => c.Firstname).ToList();
            ViewBag.Games = Context.Games.OrderBy(g => g.Title).ToList();
            ViewBag.Technicians = Context.Technicians.Where(t => t.TechnicianModelId != -1)
                .OrderBy(t => t.TechnicianModelId).ToList();
        }
    }
}
