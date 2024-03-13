using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetroRealm.Data;
using RetroRealm.Data.Services;
using RetroRealm.Models;

namespace RetroRealm.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        public ApplicationDbContext Context { get; set; }
        public CustomerController(ApplicationDbContext ctx, ICustomerService customerService)
        {
            Context = ctx;
            _customerService = customerService;
        }

        [HttpGet("customers")]
        public async Task<IActionResult> List()
        {
            List<CustomerModel> customers = await _customerService.GetAll().OrderBy(c => c.Firstname).ToListAsync();
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
            CustomerModel? customer = _customerService.GetCustomerById(id);
            ViewBag.Action = "Edit";
            return View(customer);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CustomerModel customer)
        {
            ViewBag.Countries = Context.Countries.OrderBy(c => c.Country).ToList();
            if (ModelState.IsValid)
            {
                if (customer.CustomerModelId == 0)
                    await _customerService.AddCustomer(customer);
                else
                    await _customerService.EditCustomer(customer);
                return RedirectToAction("List");
            }
            ViewBag.Action = (customer.CustomerModelId == 0) ? "Add" : "Edit";
            return View(customer);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            CustomerModel? customer = _customerService.GetCustomerById(id);
            return View(customer);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(CustomerModel customer)
        {
            await _customerService.DeleteCustomer(customer);
            return RedirectToAction("List");
        }
    }
}
