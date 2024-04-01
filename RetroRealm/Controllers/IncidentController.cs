using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetroRealm.Data;
using RetroRealm.Data.Repository;
using RetroRealm.Data.Services;
using RetroRealm.Migrations;
using RetroRealm.Models;
using System.Linq;

namespace RetroRealm.Controllers
{
    public class IncidentController : Controller
    {
        private readonly Repository<IncidentModel> _incidentDB;
        private readonly Repository<CustomerModel> _customerDB;
        private readonly Repository<GameModel> _gameDB;
        private readonly Repository<TechnicianModel> _technicianDB;
        public IncidentController(ApplicationDbContext ctx)
        {
            _incidentDB = new Repository<IncidentModel>(ctx);
            _customerDB = new Repository<CustomerModel>(ctx);
            _gameDB = new Repository<GameModel>(ctx);
            _technicianDB = new Repository<TechnicianModel>(ctx);
        }

        [HttpGet("incidents/{id?}")]
        public IActionResult List(string id)
        {
            List<IncidentModel> incidents = _incidentDB.List(new QueryOptions<IncidentModel>()
            {
                Includes = "Customer, Technician, Game"
            }).ToList();

            if(id == "open")
                incidents = incidents.Where(i => i.DateClosed == null).ToList();
            else if(id == "unassigned")
                incidents = incidents.Where(i => i.TechnicianModelId == -1).ToList();

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
                CurrentIncident = _incidentDB.GetById(id)
            };
            return View("Edit", incident);
        }

        [HttpPost]
        public IActionResult Edit(IncidentVM incidentVM)
        {
            IncidentModel? incident = incidentVM.CurrentIncident;

            if (ModelState.IsValid)
            {
                if (incident.IncidentModelId == 0)
                    _incidentDB.Add(incident);
                else
                    _incidentDB.Update(incident);
                _incidentDB.Save();
                return RedirectToAction("List");
            }

            if (incident.DateClosed > DateTime.Now)
            {
                ModelState.AddModelError("DateClosed", "Closed date cannot be in the future.");
            }
            GetViewBagOptions();
            incidentVM.Action = (incident.IncidentModelId == 0) ? "Add" : "Edit";
            return View(incidentVM);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            IncidentModel? incident = _incidentDB.GetById(id);
            return View(incident);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(IncidentModel incident)
        {
            _incidentDB.Delete(incident);
            _incidentDB.Save();
            return RedirectToAction("List");
        }

        [HttpPost]
        public IActionResult Filter(string filter)
        {
            if (filter == string.Empty)
                return RedirectToAction("List");
            return RedirectToAction("List", new { ID = filter });
        }

        void GetViewBagOptions()
        {
            ViewBag.Customers = _customerDB.List(new QueryOptions<CustomerModel>()).ToList();
            ViewBag.Games = _gameDB.List(new QueryOptions<GameModel>()).ToList();
            ViewBag.Technicians = _technicianDB.List(new QueryOptions<TechnicianModel>()).ToList();
        }
    }
}
