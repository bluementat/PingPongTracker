using PingPongTracker.Models;

namespace PingPongTracker.Data.Interfaces;

public interface IGameRepository
{
    Task<Game> GetGameAsync(int id);
    Task<IEnumerable<Game>> GetGamesAsync();
    Task<IEnumerable<Game>> GetGamesAsync(int playerId);
    Task<Game> AddGameAsync(Game game);
    Task<Game> UpdateGameAsync(Game game);
    Task DeleteGameAsync(int id);
}
