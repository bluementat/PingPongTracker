using PingPongTracker.Data.Interfaces;
using PingPongTracker.Models;

namespace PingPongTracker.Data;

public class TeamRepository : ITeamRepository
{
    private readonly ApplicationDbContext _context;

    public TeamRepository(ApplicationDbContext context)
    {
        _context = context;
    }  

    public IEnumerable<Team> GetTeams()
    {
        return _context.Teams;
    }  

    public async Task<Team> GetTeamAsync(int id)
    {
        return await _context.Teams.FindAsync(id) ?? new Team();
    }

    public async Task<Team> AddTeamAsync(Team team)
    {
        _context.Teams.Add(team);
        await _context.SaveChangesAsync();
        return team;
    }

    public async Task<Team> UpdateTeamAsync(Team team)
    {
        _context.Teams.Update(team);
        await _context.SaveChangesAsync();
        return team;
    }

    public async Task DeleteTeamAsync(int id)
    {
        var team = await _context.Teams.FindAsync(id);
        if(team is not null)
        {
            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
        }    
    }

    public void RemoveRange(IList<Team> teams)
    {
        _context.Teams.RemoveRange(teams);
    }
}
