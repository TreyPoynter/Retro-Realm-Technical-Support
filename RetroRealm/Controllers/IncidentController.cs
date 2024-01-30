using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetroRealm.Migrations;
using RetroRealm.Models;

namespace RetroRealm.Controllers
{
    public class IncidentController : Controller
    {
        public GameModelContext Context { get; set; }
        public IncidentController(GameModelContext ctx) => Context = ctx;

        [HttpGet]
        public IActionResult List()
        {
            List <IncidentModel> incidents = Context.Incidents.Include(i => i.Customer).Include(i => i.Game).ToList();
            return View(incidents);
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.Customers = Context.Customers.OrderBy(c => c.Firstname).ToList();
            ViewBag.Games = Context.Games.OrderBy(g => g.Title).ToList();
            ViewBag.Technicians = Context.Technicians.OrderBy(t => t.Name).ToList();
            return View("Edit", new IncidentModel());
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Customers = Context.Customers.OrderBy(c => c.Firstname).ToList();
            ViewBag.Games = Context.Games.OrderBy(g => g.Title).ToList();
            ViewBag.Technicians = Context.Technicians.OrderBy(t => t.Name).ToList();
            IncidentModel? incident = Context.Incidents.Find(id);
            return View("Edit", incident);
        }

        [HttpPost]
        public IActionResult Edit(IncidentModel incident)
        {
            ViewBag.Customers = Context.Customers.OrderBy(c => c.Firstname).ToList();
            ViewBag.Games = Context.Games.OrderBy(g => g.Title).ToList();
            ViewBag.Technicians = Context.Technicians.OrderBy(t => t.Name).ToList();
            if (ModelState.IsValid)
            {
                if (incident.IncidentModelId == 0)
                    Context.Incidents.Add(incident);
                else
                    Context.Incidents.Update(incident);
                Context.SaveChanges();
                return RedirectToAction("List");
            }
            ViewBag.Action = (incident.IncidentModelId == 0) ? "Add" : "Edit";
            return View(incident);
        }
    }
}
