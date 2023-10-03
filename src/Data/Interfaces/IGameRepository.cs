using PingPongTracker.Models;

namespace PingPongTracker.Data.Interfaces;

public interface IGameRepository
{
    Task<Game> GetGameAsync(Guid id);
    int GetSeasonWinsForPlayer(Guid playerId, Guid seasonId);
    int GetSeasonTotalGames(Guid playerId, Guid seasonId);
    int GetWinsForPlayer(Guid playerId);
    int GetTotalGames(Guid playerId);
    Task<Game> AddGameAsync(Game game);
    Task<Game> UpdateGameAsync(Game game);
    Task DeleteGameAsync(int id);
}
