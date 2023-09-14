namespace PingPongTracker;

public class TeamPlayerViewModel
{
    public int TeamID { get; set; }
    public Guid PlayerId { get; set; }
    public string UserName { get; set; } = string.Empty;
}
