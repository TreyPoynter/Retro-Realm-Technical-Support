using Microsoft.AspNetCore.Mvc;
using RetroRealm.Models;

namespace RetroRealm.Controllers
{
    public class GameController : Controller
    {
        public GameModelContext Context { get; set; }
        public GameController(GameModelContext ctx) => Context = ctx;

        public ViewResult Index()
        {
            return View();
        }

        [HttpGet("games")]
        public ViewResult ManageGames()
        {
            List<GameModel> games = Context.Games.OrderBy(g => g.ReleaseDate).ToList();
            return View(games);
        }
        
        [HttpGet]
        public ViewResult Delete(int id) 
        {
            GameModel? game = Context.Games.Find(id);
            return View(game);
        }
        [HttpPost]
        public ActionResult Delete(GameModel game)
        {
            Context.Games.Remove(game);
            Context.SaveChanges();
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
            GameModel? game = Context.Games.Find(id);
            return View(game);
        }
        [HttpPost]
        public ActionResult Edit(GameModel game)
        {
            if (ModelState.IsValid)
            {
                if (game.GameModelId == 0)
                {
                    Context.Games.Add(game);
                    TempData["ToastTitle"] = "Game Added";
                    TempData["ToastMessage"] = $"{game.Title} was successfully added";
                }
                else
                    Context.Games.Update(game);
                Context.SaveChanges();
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
