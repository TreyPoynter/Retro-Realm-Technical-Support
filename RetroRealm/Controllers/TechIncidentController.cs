using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetroRealm.Data;
using RetroRealm.Migrations;
using RetroRealm.Models;

namespace RetroRealm.Controllers
{
    public class TechIncidentController : Controller
    {
        private const string SESSION_KEY = "TechnicianId";
        public ApplicationDbContext Context { get; set; }
        public TechIncidentController(ApplicationDbContext ctx) => Context = ctx;
        public IActionResult Index()
        {
            if(GetSessionTechnicianId() != null)
                return RedirectToAction("List", new { id = GetSessionTechnicianId() });

            List<TechnicianModel> technicians = Context.Technicians.Where(t => t.TechnicianModelId != -1)
            .OrderBy(g => g.Name).ToList();
            return View(technicians);
        }

        [HttpPost]
        public IActionResult GetTechnician(int id)
        {
            SaveToSession(id);
            return RedirectToAction("List", new { id = id });
        }
        [HttpPost]
        public IActionResult SwitchTechnician()
        {
            HttpContext.Session.Remove(SESSION_KEY);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("techincident/list/{id}")]
        public IActionResult List(int id)
        {
            if (id != GetSessionTechnicianId())
                SaveToSession(id);
            TechnicianModel? technician = Context.Technicians.Find(id);
            List<IncidentModel> incidents = Context.Incidents.Include(c => c.Customer).Include(c => c.Game)
                .Where(i => i.TechnicianModelId == id).ToList();

            ViewBag.TechnicianName = technician.Name;

            return View(incidents);
        }

        [HttpGet]
        [Route("techincident/edit/{id}")]
        public IActionResult Edit(int id)
        {
            IncidentModel? incident = Context.Incidents
                .Include(i => i.Technician)
                .Include(i => i.Game)
                .Include(i => i.Customer)
                .FirstOrDefault(i => i.IncidentModelId == id);
            return View("Edit", incident);
        }
        [HttpPost]
        public IActionResult Edit(IncidentModel updatedIncident)
        {
            if (ModelState.IsValid)
            {
                Context.Incidents.Update(updatedIncident);
                Context.SaveChanges();
                return RedirectToAction("List", new { id = updatedIncident.TechnicianModelId });
            }
            if (updatedIncident.DateClosed > DateTime.Now)
            {
                ModelState.AddModelError("DateClosed", "Closed date cannot be in the future.");
            }

            updatedIncident.Customer = Context.Customers.Find(updatedIncident.CustomerModelId);
            updatedIncident.Game = Context.Games.Find(updatedIncident.GameModelId);
            updatedIncident.Technician = Context.Technicians.Find(updatedIncident.TechnicianModelId);

            return View(updatedIncident);
        }

        private void SaveToSession(int id)
        {
            HttpContext.Session.SetInt32(SESSION_KEY, id);
        }

        private int? GetSessionTechnicianId()
        {
            return HttpContext.Session.GetInt32(SESSION_KEY);
        }
    }
}
