using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetroRealm.Data;
using RetroRealm.Data.Services;
using RetroRealm.Migrations;
using RetroRealm.Models;

namespace RetroRealm.Controllers
{
    public class TechIncidentController : Controller
    {
        private const string SESSION_KEY = "TechnicianId";
        private readonly ICustomerService _customerService;
        private readonly ITechnicianService _technicianService;
        private readonly IIncidentService _incidentService;
        private readonly IGameService _gameService;
        public TechIncidentController(ICustomerService customerService, 
            ITechnicianService technicianService, IIncidentService incidentService,
            IGameService gameService)
        {
            _customerService = customerService;
            _technicianService = technicianService;
            _incidentService = incidentService;
            _gameService = gameService;
        }
        public async Task<IActionResult> Index()
        {
            if(GetSessionTechnicianId() != null)
                return RedirectToAction("List", new { id = GetSessionTechnicianId() });

            List<TechnicianModel> technicians = await _technicianService.GetAll().ToListAsync();
            return View(technicians);
        }

        [HttpPost]
        public IActionResult SelectTechnician(int id)
        {
            if(id == 0)
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
            TechnicianModel? technician = _technicianService.GetById(id);
            List<IncidentModel> incidents = _incidentService.GetAll().ToList();

            ViewBag.TechnicianName = technician.Name;

            return View(incidents);
        }

        [HttpGet]
        [Route("techincident/edit/{id}")]
        public IActionResult Edit(int id)
        {
            IncidentModel? incident = _incidentService.GetById(id);
            return View("Edit", incident);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(IncidentModel updatedIncident)
        {
            if (ModelState.IsValid)
            {
                await _incidentService.UpdateIncident(updatedIncident);
                return RedirectToAction("List", new { id = updatedIncident.TechnicianModelId });
            }
            if (updatedIncident.DateClosed > DateTime.Now)
            {
                ModelState.AddModelError("DateClosed", "Closed date cannot be in the future.");
            }

            updatedIncident.Customer = _customerService.GetCustomerById(updatedIncident.CustomerModelId);
            updatedIncident.Game = _gameService.GetGameById(updatedIncident.GameModelId);
            updatedIncident.Technician = _technicianService.GetById(updatedIncident.TechnicianModelId);

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
