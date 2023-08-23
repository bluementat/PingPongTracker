﻿using PingPongTracker.Models;

namespace PingPongTracker.Data;

public interface ISeasonRepository
{
    List<Season> GetSeasons();
    Task<Season> GetSeasonById(Guid id);
    Task AddSeason(Season season);
    Task UpdateSeason(Season season);
    Task DeleteSeason(Guid id);
}