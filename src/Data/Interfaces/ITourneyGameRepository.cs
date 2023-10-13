using PingPongTracker.Models;

namespace PingPongTracker.Data.Interfaces;

public interface ITourneyGameRepository
{
    IEnumerable<TourneyGame> GetTourneyGames();    
    Task<TourneyGame> GetTourneyGameById(Guid tourneyGameId);
    Task AddTourneyGame(TourneyGame tourneyGame);    
    Task UpdateTourneyGame(TourneyGame tourneyGame);
    Task DeleteTourneyGame(Guid tourneyGameId);
    Task RemoveRange(List<TourneyGame> tourneyGames);
}
