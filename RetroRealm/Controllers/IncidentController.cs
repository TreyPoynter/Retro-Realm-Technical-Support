﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetroRealm.Data;
using RetroRealm.Data.Services;
using RetroRealm.Migrations;
using RetroRealm.Models;
using System.Linq;

namespace RetroRealm.Controllers
{
    public class IncidentController : Controller
    {
        private IIncidentService _incidentService;
        public ApplicationDbContext Context { get; set; }
        public IncidentController(ApplicationDbContext ctx, IIncidentService incidentService)
        {
            Context = ctx;
            _incidentService = incidentService;
        }

        [HttpGet("incidents/{id?}")]
        public async Task<IActionResult> List(string id = "all")
        {
            List<IncidentModel> incidents = await _incidentService.GetAll().ToListAsync();
            if(id == "open")
                incidents = incidents.Where(i => i.DateClosed == null).ToList();
            if(id == "unassigned")
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
                CurrentIncident = Context.Incidents.Find(id)
            };
            return View("Edit", incident);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(IncidentVM incidentVM)
        {
            IncidentModel? incident = incidentVM.CurrentIncident;

            if (ModelState.IsValid)
            {
                if (incident.IncidentModelId == 0)
                    await _incidentService.AddIncident(incident);
                else
                    await _incidentService.UpdateIncident(incident);
                    
                Context.SaveChanges();
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
            IncidentModel? incident = _incidentService.GetById(id);
            return View(incident);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(IncidentModel incident)
        {
            await _incidentService.DeleteIncident(incident);
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
            ViewBag.Customers = Context.Customers.OrderBy(c => c.Firstname).ToList();
            ViewBag.Games = Context.Games.OrderBy(g => g.Title).ToList();
            ViewBag.Technicians = Context.Technicians.Where(t => t.TechnicianModelId != -1)
                .OrderBy(t => t.TechnicianModelId).ToList();
        }
    }
}
