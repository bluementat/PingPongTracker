using System.Net.WebSockets;
using Microsoft.EntityFrameworkCore;
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

    public IQueryable<Game> GetGames()
    {
        return _context.Games.AsQueryable();
    }

    public async Task<int> GetSeasonWinsForPlayer(Guid playerId, Guid seasonId)
    {
        var WinsAsPlayer1 = await GetGames().CountAsync(g => g.Player1WinnerId == playerId && g.SeasonId == seasonId);
        var WinsAsPlayer2 = await GetGames().CountAsync(g => g.Player2WinnerId == playerId && g.SeasonId == seasonId);        
        return WinsAsPlayer1 + WinsAsPlayer2;
    }

    public async Task<int> GetSeasonTotalGames(Guid playerId, Guid seasonId)
    {
        return await GetGames().CountAsync(g => g.Team1Player1Id == playerId && g.SeasonId == seasonId
                    || g.Team1Player2Id == playerId && g.SeasonId == seasonId
                    || g.Team2Player1Id == playerId && g.SeasonId == seasonId
                    || g.Team2Player2Id == playerId && g.SeasonId == seasonId);
    }

    public async Task<int> GetWinsForPlayer(Guid playerId)
    {
        var WinsAsPlayer1 = await GetGames().CountAsync(g => g.Player1WinnerId == playerId);
        var WinsAsPlayer2 = await GetGames().CountAsync(g => g.Player2WinnerId == playerId);
        return WinsAsPlayer1 + WinsAsPlayer2;
    }

    public async Task<int> GetTotalGames(Guid playerId)
    {
        return await GetGames().CountAsync(g => g.Team1Player1Id == playerId || g.Team1Player2Id == playerId || g.Team2Player1Id == playerId || g.Team2Player2Id == playerId);
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
