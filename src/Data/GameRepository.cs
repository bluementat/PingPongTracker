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

    public async Task<Game> GetGameAsync(Guid id)
    {
        return await _context.Games.FindAsync(id) ?? new Game();
    }

    public int GetSeasonWinsForPlayer(Guid playerId, Guid seasonId)
    {
        var result = _context.Games.Where(g => g.Player1WinnerId == playerId && g.SeasonId == seasonId).Count();
        result += _context.Games.Where(g => g.Player2WinnerId == playerId && g.SeasonId == seasonId).Count();
        return result;
    }

    public int GetSeasonTotalGames(Guid playerId, Guid seasonId)
    {
        return _context.Games.Where(g => g.Team1Player1Id == playerId && g.SeasonId == seasonId
                    || g.Team1Player2Id == playerId && g.SeasonId == seasonId
                    || g.Team2Player1Id == playerId && g.SeasonId == seasonId
                    || g.Team2Player2Id == playerId && g.SeasonId == seasonId).Count();
    }

    public int GetWinsForPlayer(Guid playerId)
    {
        var result = _context.Games.Where(g => g.Player1WinnerId == playerId).Count();
        result += _context.Games.Where(g => g.Player2WinnerId == playerId).Count();
        return result;
    }

    public int GetTotalGames(Guid playerId)
    {
        return _context.Games.Where(g => g.Team1Player1Id == playerId || g.Team1Player2Id == playerId || g.Team2Player1Id == playerId || g.Team2Player2Id == playerId).Count();
    }
   
    public async Task<Game> AddGameAsync(Game game)
    {
        _context.Games.Add(game);
        await _context.SaveChangesAsync();
        return game;
    }

    public async Task<Game> UpdateGameAsync(Game game)
    {
        _context.Games.Update(game);
        await _context.SaveChangesAsync();
        return game;
    }

    public async Task DeleteGameAsync(int id)
    {
        var game = await _context.Games.FindAsync(id);
        _context.Games.Remove(game!);
        await _context.SaveChangesAsync();
    }    
}
