using System.ComponentModel.DataAnnotations;

namespace PingPongTracker.Models;

public class Tournament
{
    [Key]
    public Guid TournamentId { get; set; }
    [Required]    
    public DateTime TournamentDate { get; set; }
    public DateTime? TournamentEndDate { get; set; }
    public bool IsComplete { get; set; } = false;
    public string Location { get; set; } = string.Empty;
    
    public List<Player> Players { get; set; } = new();    

}
