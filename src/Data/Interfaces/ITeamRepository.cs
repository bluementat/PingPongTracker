using PingPongTracker.Models;

namespace PingPongTracker.Data.Interfaces;

public interface ITeamRepository
{
    IEnumerable<Team> GetTeams();
    Task<Team> GetTeamAsync(int id);    
    Task<Team> AddTeamAsync(Team team);
    Task<Team> UpdateTeamAsync(Team team);
    Task DeleteTeamAsync(int id);
    Task AddRangeAsync(IList<Team> teams);
    void RemoveRange(IList<Team> teams);
}
