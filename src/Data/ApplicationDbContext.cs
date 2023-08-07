using Microsoft.EntityFrameworkCore;
using PingPongTracker.Models;

namespace PingPongTracker;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<Player> Players { get; set; }
}
