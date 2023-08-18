namespace PingPongTracker;

public class PlayerStandingViewModel
{
    public int Rank { get; set; }
    public string UserName { get; set; } = string.Empty;
    public int Wins { get; set; }
    public int Losses { get; set; }
    public int TotalGames { get; set; }
    public int WinPercentage { get; set; }
   
}
