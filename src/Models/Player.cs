using System.ComponentModel.DataAnnotations;

namespace PingPongTracker.Models;

public class Player
{
    [Key]
    public Guid PlayerId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Wins { get; set; }
    public int Losses { get; set; }
    public int TotalGames { get; set; }
    public int TotalPoints { get; set; }    
    public bool Eligible { get; set; } = true; 
    public bool Active { get; set; } = true;
}
