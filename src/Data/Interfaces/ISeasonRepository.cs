using PingPongTracker.Models;

namespace PingPongTracker.Data.Interfaces;

public interface ISeasonRepository
{
    IQueryable<Season> GetSeasons();
    Task<Season> GetSeasonById(Guid id);
    Season GetActiveSeason();
    Task AddSeason(Season season);
    Task UpdateSeason(Season season);
    Task DeleteSeason(Guid id);
}
