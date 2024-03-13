using Microsoft.AspNetCore.Mvc;
using RetroRealm.Data;
using RetroRealm.Data.Services;
using RetroRealm.Models;

namespace RetroRealm.Controllers
{
    public class ValidationController : Controller
    {
        private readonly ICustomerService _customerService;

        public ValidationController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public JsonResult CustomerEmailExists(string Email, int CustomerModelId)
        {
            CustomerModel? custWithEmail = _customerService.GetCustomerByEmail(Email);

            if (custWithEmail == null)  // Email is free
                return Json(true);

            if(custWithEmail.CustomerModelId != CustomerModelId)  // Email is used
                return Json("Email already exists");

            return Json(true);  // Failsafe
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
