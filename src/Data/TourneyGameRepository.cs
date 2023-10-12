using PingPongTracker.Data.Interfaces;
using PingPongTracker.Models;


namespace PingPongTracker.Data;

public class TourneyGameRepository : ITourneyGameRepository
{
    private readonly ApplicationDbContext _context;
    
    public TourneyGameRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<TourneyGame> GetTourneyGames()
    {
        return _context.TourneyGames;
    }

    public async Task<TourneyGame> GetTourneyGameById(Guid tourneyGameId)
    {
        return await _context.TourneyGames.FindAsync(tourneyGameId) ?? new TourneyGame();
    }

    public async Task AddTourneyGame(TourneyGame tourneyGame)
    {
        await _context.TourneyGames.AddAsync(tourneyGame);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTourneyGame(TourneyGame tourneyGame)
    {
        _context.TourneyGames.Update(tourneyGame);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTourneyGame(Guid tourneyGameId)
    {
        var tourneyGame = await GetTourneyGameById(tourneyGameId);
        _context.TourneyGames.Remove(tourneyGame);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveRange(List<TourneyGame> tourneyGames)
    {
        _context.TourneyGames.RemoveRange(tourneyGames);
        await _context.SaveChangesAsync();
    }    
}
