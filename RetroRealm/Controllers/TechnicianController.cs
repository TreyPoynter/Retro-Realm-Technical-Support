using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetroRealm.Data;
using RetroRealm.Data.Services;
using RetroRealm.Models;

namespace RetroRealm.Controllers
{
    public class TechnicianController : Controller
    {
        private readonly ITechnicianService _technicianService;
        public TechnicianController(ITechnicianService technicianService)
        {
            _technicianService = technicianService;
        }

        [HttpGet("technicians")]
        public async Task<IActionResult> List()
        {
            List<TechnicianModel> technicians = await _technicianService.GetAll().ToListAsync();
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
            TechnicianModel? technician = _technicianService.GetById(id);
            return View("Edit", technician);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TechnicianModel technician)
        {
            if (ModelState.IsValid)
            {
                if (technician.TechnicianModelId == 0)
                    await _technicianService.Add(technician);
                else
                    await _technicianService.Update(technician);
                return RedirectToAction("List");
            }
            ViewBag.Action = (technician.TechnicianModelId == 0) ? "Add" : "Edit";
            return View(technician);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            TechnicianModel? technician = _technicianService.GetById(id);
            return View(technician);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(TechnicianModel technician)
        {
            await _technicianService.Remove(technician);
            return RedirectToAction("List");
        }
    }
}
