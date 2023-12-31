﻿using Microsoft.EntityFrameworkCore;
using PingPongTracker.Models;

namespace PingPongTracker.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Player> Players { get; set; }
    public DbSet<Season> Seasons { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<TourneyGame> TourneyGames { get; set; }
}
