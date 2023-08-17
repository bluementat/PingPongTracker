using PingPongTracker.Models;

namespace PingPongTracker.Data;

public interface IPlayerRepository
{
    List<Player> GetPlayers();
    Task<Player> GetPlayerById(Guid id);
    Player GetPlayerByUserName(string username);
    Task AddPlayer(Player player);
    Task UpdatePlayer(Player player);
    Task DeletePlayer(Guid id);    
}
