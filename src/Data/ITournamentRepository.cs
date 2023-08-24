using PingPongTracker.Models;

namespace PingPongTracker.Data;

public interface ITournamentRepository
{
    Task<Tournament> GetTournamentAsync(int id);
    Task<IEnumerable<Tournament>> GetTournamentsAsync();
    Task<Tournament> AddTournamentAsync(Tournament tournament);
    Task<Tournament> UpdateTournamentAsync(Tournament tournament);
    Task<Tournament> DeleteTournamentAsync(int id);
}
