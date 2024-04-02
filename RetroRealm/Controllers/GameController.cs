using Microsoft.AspNetCore.Mvc;
using RetroRealm.Data;
using RetroRealm.Data.Repository;
using RetroRealm.Models;

namespace RetroRealm.Controllers
{
    public class GameController : Controller
    {
        private readonly Repository<GameModel> _gameDB;
        public GameController(ApplicationDbContext ctx)
        {
            _gameDB = new Repository<GameModel>(ctx);
        }

        [HttpGet("games")]
        public ViewResult ManageGames()
        {
            List<GameModel> games = _gameDB.List(new QueryOptions<GameModel>()
            {

            }).ToList();
            return View(games);
        }
        
        [HttpGet]
        public ViewResult Delete(int id) 
        {
            GameModel? game = _gameDB.GetById(id);
            return View(game);
        }
        [HttpPost]
        public ActionResult Delete(GameModel game)
        {
            _gameDB.Delete(game);
            _gameDB.Save();
            return RedirectToAction("ManageGames");
        }

        [HttpGet]
        public ViewResult Add()
        {
            ViewBag.Action = "Add";
            return View("Edit", new GameModel());
        }
        [HttpGet]
        public ViewResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            GameModel? game = _gameDB.GetById(id);
            return View(game);
        }
        [HttpPost]
        public ActionResult Edit(GameModel game)
        {
            if (ModelState.IsValid)
            {
                if (game.GameModelId == 0)
                {
                    _gameDB.Add(game);
                    TempData["ToastTitle"] = "Game Added";
                    TempData["ToastMessage"] = $"{game.Title} was successfully added";
                }
                else
                    _gameDB.Update(game);
                _gameDB.Save();
                return RedirectToAction("ManageGames");
            }
            ViewBag.Action = (game.GameModelId == 0) ? "Add" : "Edit";
            return View(game);
        }

        [HttpPost]
        public ActionResult ClearTempData()
        {
            TempData.Clear();
            return RedirectToAction("ManageGames");
        }
    }
}
