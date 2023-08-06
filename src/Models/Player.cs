namespace PingPongTracker.Models;

public class Player
{
    public Guid PlayerId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Wins { get; set; }
    public int Losses { get; set; }
    public int TotalGames { get; set; }
    public int TotalPoints { get; set; }    
}
