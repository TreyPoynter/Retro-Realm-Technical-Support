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
        private readonly ICountriesService _countryService;
        public CustomerController(ICustomerService customerService, ICountriesService countryService)
        {
            _countryService = countryService;
            _customerService = customerService;
        }

        [HttpGet("customers")]
        public async Task<IActionResult> List()
        {
            List<CustomerModel> customers = await _customerService.GetAll().OrderBy(c => c.Firstname).ToListAsync();
            return View(customers);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            ViewBag.Countries = await _countryService.GetCountries().ToListAsync();
            ViewBag.Action = "Add";
            return View("Edit", new CustomerModel());
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Countries = await _countryService.GetCountries().ToListAsync();
            CustomerModel? customer = _customerService.GetCustomerById(id);
            ViewBag.Action = "Edit";
            return View(customer);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CustomerModel customer)
        {
            if (ModelState.IsValid)
            {
                if (customer.CustomerModelId == 0)
                    await _customerService.AddCustomer(customer);
                else
                    await _customerService.EditCustomer(customer);
                return RedirectToAction("List");
            }
            ViewBag.Countries = await _countryService.GetCountries().ToListAsync();
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
