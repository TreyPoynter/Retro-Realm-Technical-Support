using Microsoft.AspNetCore.Mvc;
using RetroRealm.Models;

namespace RetroRealm.Controllers
{
    public class CustomerController : Controller
    {
        [HttpGet]
        public IActionResult List()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("Edit", new CustomerModel());
        }

        [HttpGet]
        public IActionResult Edit()
        {
            ViewBag.Action = "Edit";
            return View();
        }

        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }
    }
}
