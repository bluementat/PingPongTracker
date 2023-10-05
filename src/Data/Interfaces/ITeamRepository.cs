using PingPongTracker.Models;

namespace PingPongTracker.Data.Interfaces;

public interface ITeamRepository
{
    IQueryable<Team> GetTeams();
    Task<Team> GetTeamAsync(int id);    
    Task<Team> AddTeamAsync(Team team);
    Task<Team> UpdateTeamAsync(Team team);
    Task DeleteTeamAsync(int id);
}
