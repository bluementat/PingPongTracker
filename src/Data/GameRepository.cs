using PingPongTracker.Data.Interfaces;
using PingPongTracker.Models;

namespace PingPongTracker.Data;

public class GameRepository : IGameRepository
{
    private readonly ApplicationDbContext _context;

    public GameRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Game> AddGameAsync(Game game)
    {
        _context.Games.Add(game);
        await _context.SaveChangesAsync();
        return game;
    }

    public async Task DeleteGameAsync(int id)
    {
        var game = await _context.Games.FindAsync(id);
        _context.Games.Remove(game!);
        await _context.SaveChangesAsync();
    }

    public async Task<Game> GetGameAsync(int id)
    {
        return await _context.Games.FindAsync(id);
    }

    public async Task<IEnumerable<Game>> GetGamesAsync()
    {
        return await _context.Games.ToListAsync();
    }

    public async Task<IEnumerable<Game>> GetGamesAsync(int playerId)
    {
        return await _context.Games.Where(g => g.Player1Id == playerId || g.Player2Id == playerId).ToListAsync();
    }

    public async Task<Game> UpdateGameAsync(Game game)
    {
        _context.Entry(game).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return game;
    }
}
