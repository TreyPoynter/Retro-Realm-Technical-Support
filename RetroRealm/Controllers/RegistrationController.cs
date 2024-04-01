using Microsoft.AspNetCore.Mvc;
using RetroRealm.Data.Services;
using RetroRealm.Models;

namespace RetroRealm.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly ICustomerService _customerService;

        public RegistrationController(ICustomerService customerService, IGameService gameService)
        {
            _customerService = customerService;

        }

        public IActionResult Index()
        {
            List<CustomerModel> customers = _customerService.GetAll().ToList();
            return View(customers);
        }

        public IActionResult List(int id)
        {
            CustomerModel? customer = _customerService.GetCustomerById(id);

            return View(customer);
        }
        [HttpPost]
        public IActionResult SelectCustomer(int id)
        {
            if (id == 0)  // user didn't select a technician (safe guard)
            {
                TempData["Error"] = "You must select a customer";
                return RedirectToAction("Index");
            }

            return RedirectToAction("List", new { id });
        }
    }
}
