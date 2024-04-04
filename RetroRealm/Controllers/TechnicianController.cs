using Microsoft.AspNetCore.Mvc;
using RetroRealm.Data;
using RetroRealm.Data.Repository;
using RetroRealm.Models;

namespace RetroRealm.Controllers
{
    public class TechnicianController : Controller
    {
        private readonly Repository<TechnicianModel> _technicianDb;
        public TechnicianController(ApplicationDbContext ctx)
        {
            _technicianDb = new Repository<TechnicianModel>(ctx);
        }

        [HttpGet("technicians")]
        public IActionResult List()
        {
            List<TechnicianModel> technicians = _technicianDb.List(
                new QueryOptions<TechnicianModel>()
                {
                    Where = t => t.TechnicianModelId != -1
                }).ToList();
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
            TechnicianModel? technician = _technicianDb.GetById(id);
            return View("Edit", technician);
        }

        [HttpPost]
        public IActionResult Edit(TechnicianModel technician)
        {
            if (ModelState.IsValid)
            {
                if (technician.TechnicianModelId == 0)
                    _technicianDb.Add(technician);
                else
                    _technicianDb.Update(technician);
                _technicianDb.Save();
                return RedirectToAction("List");
            }
            ViewBag.Action = (technician.TechnicianModelId == 0) ? "Add" : "Edit";
            return View(technician);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            TechnicianModel? technician = _technicianDb.GetById(id);
            return View(technician);
        }
        [HttpPost]
        public IActionResult Delete(TechnicianModel technician)
        {
            _technicianDb.Delete(technician);
            _technicianDb.Save();
            return RedirectToAction("List");
        }
    }
}
