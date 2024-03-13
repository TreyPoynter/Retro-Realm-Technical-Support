using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetroRealm.Data;
using RetroRealm.Models;

namespace RetroRealm.Controllers
{
    public class CustomerController : Controller
    {
        public ApplicationDbContext Context { get; set; }
        public CustomerController(ApplicationDbContext ctx) => Context = ctx;

        [HttpGet("customers")]
        public IActionResult List()
        {
            List<CustomerModel> customers = Context.Customers.Include(c => c.CountryModel).OrderBy(c => c.Firstname).ToList();
            return View(customers);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Countries = Context.Countries.OrderBy(c => c.Country).ToList();
            ViewBag.Action = "Add";
            return View("Edit", new CustomerModel());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Countries = Context.Countries.OrderBy(c => c.Country).ToList();
            CustomerModel? customer = Context.Customers.Find(id);
            ViewBag.Action = "Edit";
            return View(customer);
        }
        [HttpPost]
        public IActionResult Edit(CustomerModel customer)
        {
            ViewBag.Countries = Context.Countries.OrderBy(c => c.Country).ToList();
            if (ModelState.IsValid)
            {
                if (customer.CustomerModelId == 0)
                    Context.Customers.Add(customer);
                else
                    Context.Customers.Update(customer);
                Context.SaveChanges();
                return RedirectToAction("List");
            }
            ViewBag.Action = (customer.CustomerModelId == 0) ? "Add" : "Edit";
            return View(customer);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            CustomerModel? customer = Context.Customers.Find(id);
            return View(customer);
        }
        [HttpPost]
        public IActionResult Delete(CustomerModel customer)
        {
            Context.Customers.Remove(customer);
            Context.SaveChanges();
            return RedirectToAction("List");
        }
    }
}
