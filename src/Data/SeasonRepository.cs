﻿using Microsoft.EntityFrameworkCore;
using PingPongTracker.Data;
using PingPongTracker.Data.Interfaces;
using PingPongTracker.Models;

namespace PingPongTracker.Data;

public class SeasonRepository : ISeasonRepository
{
    private readonly ApplicationDbContext _context;

    public SeasonRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IQueryable<Season> GetSeasons()
    {
        return _context.Seasons;
    }

    public async Task<Season> GetSeasonById(Guid id)
    {
        return await _context.Seasons.FindAsync(id) ?? new Season();
    }

    public Season? GetActiveSeason()
    {
        return _context.Seasons.Where(s => s.Active).FirstOrDefault();
    }

    public async Task AddSeason(Season season)
    {
        // If the new season is active, deactivate all other seasons
        if (season.Active)
        {
            var seasons = await _context.Seasons.ToListAsync();
            foreach (var s in seasons)
            {
                s.Active = false;
            }
        }

        await _context.Seasons.AddAsync(season);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateSeason(Season season)
    {
        // If the updated season is active, deactivate all other seasons
        if (season.Active)
        {
            var seasons = await _context.Seasons.AsNoTracking().ToListAsync();
            foreach (var s in seasons)
            {
                if(s.Active && s.SeasonId != season.SeasonId)
                {
                    s.Active = false;
                    _context.Seasons.Update(s);
                }
                
            }
        }
        
        _context.Seasons.Update(season);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteSeason(Guid id)
    {
        var season = await GetSeasonById(id);
        _context.Seasons.Remove(season);
        await _context.SaveChangesAsync();
    }
}
