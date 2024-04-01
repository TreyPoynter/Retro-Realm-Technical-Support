using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetroRealm.Data;
using RetroRealm.Data.Services;
using RetroRealm.Migrations;
using RetroRealm.Models;

namespace RetroRealm.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IGameService _gameService;

        public RegistrationController(ICustomerService customerService, IGameService gameService)
        {
            _customerService = customerService;
            _gameService = gameService;

        }

        public IActionResult Index()
        {
            List<CustomerModel> customers = _customerService.GetAll().ToList();
            return View(customers);
        }

        public IActionResult List(int id)
        {
            CustomerModel? customer = _customerService.GetCustomerById(id);

            ViewBag.Games = _gameService.GetAll().ToList();

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

        [HttpPost]
        public async Task<IActionResult> RegisterGame(int id, int customerId)
        {
            CustomerModel? customer = _customerService.GetCustomerById(customerId);
            GameModel? game = _gameService.GetGameById(id);

            customer.GameModels.Add(game);

            await _customerService.EditCustomer(customer);

            return RedirectToAction("List", new { id = customerId });
        }

        public IActionResult Delete(int custId, int id)
        {
            GameModel? game = _gameService.GetGameById(id);
            CustomerModel? customer = _customerService.GetCustomerById(custId);
            ViewBag.Game = game;
            return View(customer);
        }
        [HttpPost]
        public IActionResult Delete(int custId, uint gameId)
        {
            CustomerModel? customer = _customerService.GetCustomerById(custId);
            GameModel game = _gameService.GetGameById((int)gameId);
            customer.GameModels.Remove(game);
            _customerService.EditCustomer(customer);
            return RedirectToAction("List", new { id = custId });
        }
    }
}
