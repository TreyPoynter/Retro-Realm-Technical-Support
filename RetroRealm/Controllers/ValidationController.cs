using Microsoft.AspNetCore.Mvc;
using RetroRealm.Data;
using RetroRealm.Models;

namespace RetroRealm.Controllers
{
    public class ValidationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ValidationController(ApplicationDbContext ctx)
        {
            _context = ctx;
        }

        public JsonResult CustomerEmailExists(string Email, int CustomerModelId)
        {
            CustomerModel? custWithEmail = _context.Customers.FirstOrDefault(c => c.Email == Email);

            if (custWithEmail == null)  // Email is free
                return Json(true);
            return Json("Email already exists");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
