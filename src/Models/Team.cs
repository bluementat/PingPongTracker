namespace PingPongTracker.Models;

public class Team
{
    public int TeamID { get; set; }
    public Guid Player1Id { get; set; }
    public string Player1UserName { get; set; } = string.Empty;
    public Guid Player2Id { get; set; }
    public string Player2UserName { get; set; } = string.Empty;

}
