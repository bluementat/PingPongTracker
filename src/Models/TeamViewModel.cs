namespace PingPongTracker;

public class TeamViewModel
{
    public string TeamName { get; set; } = string.Empty;
    public Guid Player1Id { get; set; }
    public Guid Player2Id { get; set; }
    public int Score { get; set; }
}
