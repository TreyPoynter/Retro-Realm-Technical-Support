using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetroRealm.Data;
using RetroRealm.Data.Services;
using RetroRealm.Models;

namespace RetroRealm.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameService _gameService;
        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet("games")]
        public async Task<ViewResult> ManageGames()
        {
            List<GameModel> games = await _gameService.GetAll().ToListAsync();
            return View(games);
        }
        
        [HttpGet]
        public ViewResult Delete(int id) 
        {
            GameModel? game = _gameService.GetGameById(id);
            return View(game);
        }
        [HttpPost]
        public ActionResult Delete(GameModel game)
        {
            _gameService.DeleteGame(game);
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
            GameModel? game = _gameService.GetGameById(id);
            return View(game);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(GameModel game)
        {
            if (ModelState.IsValid)
            {
                if (game.GameModelId == 0)
                {
                    await _gameService.AddGame(game);
                    TempData["ToastTitle"] = "Game Added";
                    TempData["ToastMessage"] = $"{game.Title} was successfully added";
                }
                else
                    await _gameService.UpdateGame(game);
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
