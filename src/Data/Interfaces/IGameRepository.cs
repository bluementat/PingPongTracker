using PingPongTracker.Models;

namespace PingPongTracker.Data.Interfaces;

public interface IGameRepository
{
    Task<Game> GetGameAsync(Guid id);
    IQueryable<Game> GetGames();
    Task<int> GetSeasonWinsForPlayer(Guid playerId, Guid seasonId);
    Task<int> GetSeasonTotalGames(Guid playerId, Guid seasonId);
    Task<int> GetWinsForPlayer(Guid playerId);
    Task<int> GetTotalGames(Guid playerId);
    Task<Game> AddGameAsync(Game game);
    Task<Game> UpdateGameAsync(Game game);
    Task DeleteGameAsync(Guid id);
}
