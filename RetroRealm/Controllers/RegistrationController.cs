using Microsoft.AspNetCore.Mvc;
using RetroRealm.Data;
using RetroRealm.Data.Repository;
using RetroRealm.Models;

namespace RetroRealm.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly CustomerRepository _customerDB;
        private readonly Repository<GameModel> _gameDB;

        public RegistrationController(ApplicationDbContext ctx)
        {
            _customerDB = new CustomerRepository(ctx);
            _gameDB = new Repository<GameModel>(ctx);
        }

        public IActionResult Index()
        {
            List<CustomerModel> customers = _customerDB.List(new QueryOptions<CustomerModel>()).ToList();
            return View(customers);
        }

        public IActionResult List(int id)
        {
            CustomerModel? customer = _customerDB.GetById(id);

            ViewBag.Games = _gameDB.List(new QueryOptions<GameModel>()).ToList();

            return View(customer);
        }
        [HttpPost]
        public IActionResult SelectCustomer(int id)
        {
            if (id == 0)  // user didn't select a customer (safe guard)
            {
                TempData["Error"] = "You must select a customer";
                return RedirectToAction("Index");
            }

            return RedirectToAction("List", new { id });
        }

        [HttpPost]
        public IActionResult RegisterGame(int id, int customerId)
        {
            CustomerModel? customer = _customerDB.GetById(customerId);
            GameModel? game = _gameDB.GetById(id);

            customer.GameModels.Add(game);

            _customerDB.Update(customer);
            _customerDB.Save();

            return RedirectToAction("List", new { id = customerId });
        }

        public IActionResult Delete(int custId, int id)
        {
            GameModel? game = _gameDB.GetById(id);
            CustomerModel? customer = _customerDB.GetById(custId);
            ViewBag.Game = game;
            return View(customer);
        }
        [HttpPost]
        public IActionResult DeleteFromCustomer(int custId, int gameId)
        {
            CustomerModel? customer = _customerDB.GetById(custId);
            GameModel? game = _gameDB.GetById((int)gameId);
            customer.GameModels.Remove(game);
            _customerDB.Update(customer);
            _customerDB.Save();
            return RedirectToAction("List", new { id = custId });
        }
    }
}
