using RetroRealm.Models;

namespace RetroRealm.Data.Services
{
    public interface IGameService
    {
        IQueryable<GameModel> GetAll();
        Task DeleteGame(GameModel game);
        Task UpdateGame(GameModel game);
        Task AddGame(GameModel game);
        GameModel? GetGameById(int? id);

    }
}
