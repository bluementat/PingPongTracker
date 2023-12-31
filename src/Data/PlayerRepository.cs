﻿using Microsoft.EntityFrameworkCore;
using PingPongTracker.Data.Interfaces;
using PingPongTracker.Models;

namespace PingPongTracker.Data;

public class PlayerRepository : IPlayerRepository
{
    private readonly ApplicationDbContext _dbContext;

    public PlayerRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Player>> GetPlayers()
    {
        return await _dbContext.Players.ToListAsync();
    }

    public async Task<Player> GetPlayerById(Guid id)
    {
        return await _dbContext.Players.FindAsync(id) ?? new Player();
    }

    public Player GetPlayerByUserName(string username)
    {
        return _dbContext.Players.FirstOrDefault(p => p.UserName == username) ?? new Player();
    }

    public string GetUserNameById(Guid id)
    {
        return _dbContext.Players.Find(id)?.UserName ?? "";
    }

    public bool PlayerUserNameChanged(Player player)
    {
        var playerFromDb = _dbContext.Players.Find(player.PlayerId) ?? new Player();
        return playerFromDb.UserName != player.UserName;
    }

    public IEnumerable<Player> GetActivePlayers()
    {
        return _dbContext.Players.Where(p => p.Active);
    }

    public bool GoodUserNameChange(Player player)
    {
        var playerFromDb = _dbContext.Players.AsNoTracking().FirstOrDefault(p => p.PlayerId == player.PlayerId) ?? new Player();
        if (playerFromDb.UserName != player.UserName)
        {
            return _dbContext.Players.FirstOrDefault(p => p.UserName == player.UserName) is null;
        }
        return true;
    }

    public async Task AddPlayer(Player player)
    {
        _dbContext.Players.Add(player);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdatePlayer(Player player)
    {
        _dbContext.Players.Update(player);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeletePlayer(Guid id)
    {
        var player = _dbContext.Players.Find(id);
        if (player is not null)
        {
            _dbContext.Players.Remove(player);
            await _dbContext.SaveChangesAsync();
        }
    }
}