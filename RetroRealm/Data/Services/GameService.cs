using RetroRealm.Models;

namespace RetroRealm.Data.Services
{
    public class GameService : IGameService
    {
        private readonly ApplicationDbContext _context;

        public GameService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddGame(GameModel game)
        {
            _context.Games.Add(game);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGame(GameModel game)
        {
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
        }

        public IQueryable<GameModel> GetAll()
        {
            IQueryable<GameModel> games = _context.Games;
            return games;
        }

        public GameModel? GetGameById(int id)
        {
            GameModel? game = _context.Games.FirstOrDefault(g => g.GameModelId == id);
            return game;
        }

        public async Task UpdateGame(GameModel game)
        {
            _context.Games.Update(game);
            await _context.SaveChangesAsync();
        }
    }
}
