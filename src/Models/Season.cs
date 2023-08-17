using System.ComponentModel.DataAnnotations;

namespace PingPongTracker.Models;

public class Season
{
    [Key]
    public Guid SeasonId { get; set; }
    [Required]
    public DateTime SeasonStart { get; set; }
    [Required]
    public DateTime SeasonEnd { get; set; }
    
    public List<Tournament> Tournaments { get; set; } = new();   
}
