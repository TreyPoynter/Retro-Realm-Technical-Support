using Microsoft.AspNetCore.Mvc;
using RetroRealm.Data;
using RetroRealm.Data.Repository;
using RetroRealm.Models;

namespace RetroRealm.Controllers
{
    public class TechIncidentController : Controller
    {
        private const string SESSION_KEY = "TechnicianId";
        private readonly Repository<CustomerModel> _customerDB;
        private readonly Repository<TechnicianModel> _technicianDB;
        private readonly Repository<IncidentModel> _incidentDB;
        private readonly Repository<GameModel> _gameDB;
        public TechIncidentController(ApplicationDbContext ctx)
        {
            _customerDB = new Repository<CustomerModel>(ctx);
            _technicianDB = new Repository<TechnicianModel>(ctx);
            _incidentDB = new Repository<IncidentModel>(ctx);
            _gameDB = new Repository<GameModel>(ctx);
        }
        public async Task<IActionResult> Index()
        {
            if(GetSessionTechnicianId() != null)  // If the user already selected a technician
                return RedirectToAction("List", new { id = GetSessionTechnicianId() });

            List<TechnicianModel> technicians = _technicianDB.List(new QueryOptions<TechnicianModel>()).ToList();
            return View(technicians);
        }

        [HttpPost]
        public IActionResult SelectTechnician(int id)
        {
            if(id == 0)  // user didn't select a technician (safe guard)
            {
                TempData["Error"] = "You must select a technician";
                return RedirectToAction("Index");
            }

            SaveToSession(id);
            return RedirectToAction("List", new { id });
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
            TechnicianModel? technician = _technicianDB.GetById(id);
            List<IncidentModel> incidents = _incidentDB.List(new QueryOptions<IncidentModel>()).ToList();

            ViewBag.TechnicianName = technician.Name;

            return View(incidents);
        }

        [HttpGet]
        [Route("techincident/edit/{id}")]
        public IActionResult Edit(int id)
        {
            IncidentModel? incident = _incidentDB.GetById(id);
            return View("Edit", incident);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(IncidentModel updatedIncident)
        {
            if (ModelState.IsValid)
            {
                _incidentDB.Update(updatedIncident);
                _incidentDB.Save();
                return RedirectToAction("List", new { id = updatedIncident.TechnicianModelId });
            }
            if (updatedIncident.DateClosed > DateTime.Now)
            {
                ModelState.AddModelError("DateClosed", "Closed date cannot be in the future.");
            }

            updatedIncident.Customer = _customerDB.GetById((int)updatedIncident.CustomerModelId);
            updatedIncident.Game = _gameDB.GetById((int)updatedIncident.GameModelId);
            updatedIncident.Technician = _technicianDB.GetById((int)updatedIncident.TechnicianModelId);

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
