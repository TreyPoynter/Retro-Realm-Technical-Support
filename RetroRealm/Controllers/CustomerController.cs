using Microsoft.AspNetCore.Mvc;
using RetroRealm.Data;
using RetroRealm.Data.Repository;
using RetroRealm.Models;

namespace RetroRealm.Controllers
{
    public class CustomerController : Controller
    {
        private readonly Repository<CustomerModel> _customerDB;
        private readonly Repository<CountryModel> _countryDB;
        public CustomerController(ApplicationDbContext ctx)
        {
            _customerDB = new Repository<CustomerModel>(ctx);
            _countryDB = new Repository<CountryModel>(ctx);
        }

        [HttpGet("customers")]
        public IActionResult List()
        {
            List<CustomerModel> customers = _customerDB.List(new QueryOptions<CustomerModel>())
                .OrderBy(c => c.Firstname).ToList();
            return View(customers);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Countries = _countryDB.List(new QueryOptions<CountryModel>()).ToList();
            ViewBag.Action = "Add";
            return View("Edit", new CustomerModel());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Countries = _countryDB.List(new QueryOptions<CountryModel>()).ToList();
            CustomerModel? customer = _customerDB.GetById(id);
            ViewBag.Action = "Edit";
            return View(customer);
        }
        [HttpPost]
        public IActionResult Edit(CustomerModel customer)
        {
            if (ModelState.IsValid)
            {
                if (customer.CustomerModelId == 0)
                    _customerDB.Add(customer);
                else
                    _customerDB.Update(customer);
                _customerDB.Save();
                return RedirectToAction("List");
            }
            ViewBag.Countries = _countryDB.List(new QueryOptions<CountryModel>()).ToList();
            ViewBag.Action = (customer.CustomerModelId == 0) ? "Add" : "Edit";
            return View(customer);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            CustomerModel? customer = _customerDB.GetById(id);
            return View(customer);
        }
        [HttpPost]
        public IActionResult Delete(CustomerModel customer)
        {
            _customerDB.Delete(customer);
            _customerDB.Save();
            return RedirectToAction("List");
        }
    }
}
