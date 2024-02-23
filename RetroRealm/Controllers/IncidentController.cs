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
            List <IncidentModel> incidents = Context.Incidents.Include(c => c.Customer).Include(g => g.Game).ToList();
            IncidentVM incidentVM = new()
            {
                Incidents = incidents,
                Filter = "All"
            };
            return View(incidentVM);
        }
        [HttpGet]
        public IActionResult Add()
        {
            GetViewBagOptions();
            return View("Edit", new IncidentVM() { Action = "Add" });
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            GetViewBagOptions();
            IncidentVM? incident = new()
            {
                Action = "Edit",
                CurrentIncident = Context.Incidents.Find(id)
            };
            return View("Edit", incident);
        }

        [HttpPost]
        public IActionResult Edit(IncidentVM incidentVM)
        {
            GetViewBagOptions();
            if (ModelState.IsValid)
            {
                if (incidentVM.CurrentIncident.IncidentModelId == 0)
                    Context.Incidents.Add(incidentVM.CurrentIncident);
                else
                    Context.Incidents.Update(incidentVM.CurrentIncident);
                Context.SaveChanges();
                return RedirectToAction("List");
            }
            if (incidentVM.CurrentIncident.DateClosed > DateTime.Now)
            {
                ModelState.AddModelError("DateClosed", "Closed date cannot be in the future.");
            }
            incidentVM.Action = (incidentVM.CurrentIncident.IncidentModelId == 0) ? "Add" : "Edit";
            return View(incidentVM.CurrentIncident);
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
