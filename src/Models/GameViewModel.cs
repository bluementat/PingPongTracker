namespace PingPongTracker.Models;

public class GameViewModel
{
    public Guid GameId { get; set; }
    public int Team1Id { get; set; }
    public int Team2Id { get; set; }
    public string Team1Name { get; set; } = "";
    public string Team2Name { get; set; } = "";
    public int Team1Score { get; set; }
    public int Team2Score { get; set; }
}
